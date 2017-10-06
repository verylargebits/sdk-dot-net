/* Copyright(c) 2017, Very Large Bits LLC

MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this
software and associated documentation files (the "Software"), to deal in the Software
without restriction, including without limitation the rights to use, copy, modify,
merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be included in all copies
or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using VeryLargeBits.Schema;
using VeryLargeBits.Extensions.Samples;
using System.Collections.Generic;

namespace VeryLargeBits.AssetTool
{
    static partial class Program
    {
        static string apiKey, assetId, email, filename, password, privateKeyFilename;
        static byte[] hash;
        static bool help, status, uploaded, verbose;
        static int patchCount, patchSize;
        static AssetStatus? statusValue;
        static DateTime started;
        static Client svc;
        static int? wait;

        static void Main(string[] args)
        {
            // Parse the arguments
            apiKey = args.ParseArg("-k", "--key");
            email = args.ParseArg("-e", "--email");
            password = args.ParseArg("-p", "--password");
            patchSize = (args.ParseArg("--patch-size") ?? "4MB").ParseByteSize();
            privateKeyFilename = args.ParseArg("-s", "--secret");
            status = args.ParseBool("--status");
            statusValue = args.ParseArg("--status").ParseAssetStatusOrNull();
            wait = args.ParseArg("-w", "--wait").ParseIntOrNull();
            help = args.ParseBool("-h", "--help");
            verbose = args.ParseBool("-v", "--verbose");
            filename = args.LastOrDefault();

            // Early-out with the help message if we have bad args
            if (help || null == filename || !File.Exists(filename))
            {
                PrintHelp();
                return;
            }

            // Create a service client to talk with Very Large Bits
            if (string.IsNullOrWhiteSpace(email))
                svc = string.IsNullOrWhiteSpace(privateKeyFilename) ? Client.Default(apiKey) : Client.FromPem8File(privateKeyFilename, apiKey);
            else
                svc = new Client(email, password);

            // Main logic starts here
            IdentifyFilename();

            if (string.IsNullOrEmpty(assetId))
                Upload();

            PrintGoodbye();
        }

        static void IdentifyFilename()
        {
            // Calculate the hash of 'filename'
            using (SHA1Managed sha1 = new SHA1Managed())
            using (var source = File.OpenRead(filename))
            {
                PrintComputingHash();
                hash = sha1.ComputeHash(source);
                patchCount = (int)Math.Max(0, Math.Ceiling((double)source.Length / patchSize) - 1);
                PrintKeyOverrides();
            }

            // Get the id that Very Large Bits has for 'filename'
            assetId = svc.GetAssetIdByHash(hash);
            if (verbose && string.IsNullOrEmpty(assetId))
                PrintNotFound();

            started = DateTime.Now;
        }

        static void Upload()
        {
            PrintPatchSize();

            var asset = new Asset
            {
                Hash = Convert.ToBase64String(hash),
                PatchCount = patchCount,
            };

            // Add a new asset with the first data patch
            asset.Data = new byte[patchSize].ReadFilePatch(filename, patchSize, 0);
            assetId = svc.Add(asset);
            PrintFistPatch();

            // Early-out if no patches
            if (0 == patchCount)
                return;

            // Multi-thread the data patches (except the first one which has already been sent)
            var tasks = new List<Task>();
            for (var patchIndex = 1; patchIndex <= patchCount; patchIndex++)
                tasks.Add(Task.Factory.StartNew(delegate (object state)
                {
                    var taskIndex = (int)state;
                    var buffer = new byte[patchSize].ReadFilePatch(filename, patchSize, taskIndex);

                    // Wait if requested on the last patch index
                    if (taskIndex < patchCount - 1)
                        svc.Patch(assetId, taskIndex, buffer);
                    else
                        svc.Patch(assetId, taskIndex, buffer, statusValue, wait);
                }, patchIndex));
            Task.WaitAll(tasks.ToArray());
        }        
    }
}
