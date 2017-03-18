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

namespace VeryLargeBits.AssetTool
{
    static partial class Program
    {
        static void PrintHelp()
        {
            Console.WriteLine(@"Usage: vlba [OPTION]... [FILE]...
Adds asset FILEs to the Very Large Bits system. Examples:
  vlba movie.mp4
  vlba -v movie.mp4
  vlba --patch-size 24000000 movie.mp4
  vlba --patch-size 24MB movie.mp4
  vlba --key fyn615m07rbpfmydvk3re6wwgq --secret c:\\mykeyfile.pkcs8 movie.mp4

Also blocks until a specific status is reached. Example:
  vlba -s USABLE movie.mp4

Security OPTIONs when using API Key authentication:
  -k or --key      Override the app.config API key value.
  -s or --secret   Override the app.config private key filename value.

Security OPTIONs when using basic API authentication:
  -e or --email    The email address used to sign in to dashboard.verylargebits.com.
  -p or --password Required if email is specificied.

Data OPTIONs:
  --patch-size     Override the default patch size of 4MB.

Other OPTIONs:
  -h or --help     Print this message.
  -s or --status   Checks the status if none provided or waits until the
                   provided status is reached. Only valid value is USABLE.
  -w or --wait     The number of seconds to wait if a status value is provided with -s.
                   Default is 600 (10 minutes).
  -v or --verbose  Enable detailed output.");
        }

        static void PrintComputingHash()
        {
            if (verbose)
                Console.Write("Computing hash of: {0}", filename);
        }

        static void PrintPatchSize()
        {
            uploaded = true;

            if (verbose)
            {
                Console.WriteLine("Patch byte size: {0:n0}", patchSize);
                Console.Write("Uploading {0} patch{1}: ", patchCount, 1 == patchCount ? string.Empty : "es");
            }
        }

        static void PrintFistPatch()
        {
            if (verbose)
            {
                
            }
        }

        static void PrintKeyOverrides()
        {
            if (!verbose)
                return;

            Console.WriteLine();

            if (null != apiKey)
                Console.WriteLine("Overriding app.config API key setting with: {0}", apiKey);

            if (null != privateKeyFilename)
                Console.WriteLine("Overriding app.config private key file setting with: {0}", privateKeyFilename);
        }

        static void PrintNotFound()
        {
            Console.WriteLine("'{0}' was not found in the Very Large Bits system", filename);
        }

        static void PrintGoodbye()
        {
            if (verbose && uploaded)
            {
                var elapsed = DateTime.Now - started;
                var megabyte = 1024 * 1024;
                var megabytes = new FileInfo(filename).Length / megabyte;
                Console.WriteLine("done");
                Console.WriteLine("Upload took {0:n2}s ({1:n2} Mb/s)", elapsed.TotalSeconds, megabytes / elapsed.TotalSeconds);
            }

            Console.WriteLine(assetId);
        }
    }
}
