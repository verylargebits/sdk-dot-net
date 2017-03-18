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

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VeryLargeBits;
using VeryLargeBits.Extensions.Samples;
using VeryLargeBits.Schema;

namespace VeryLargeBits.TemplateTool
{
    static partial class Program
    {
        static string apiKey, email, filename, id, password, privateKeyFilename;
        static bool help, status, verbose;
        static RenderStatus? statusValue;
        static DateTime started;
        static Client svc;
        static int? wait;

        static void Main(string[] args)
        {
            // Parse the arguments
            apiKey = args.ParseArgIncludingLast("-k", "--key");
            email = args.ParseArgIncludingLast("-e", "--email");
            id = args.ParseArgIncludingLast("-r", "--render");
            password = args.ParseArgIncludingLast("-p", "--password");
            privateKeyFilename = args.ParseArgIncludingLast("-s", "--secret");
            statusValue = args.ParseArgIncludingLast("-s", "--status").ParseRenderStatusOrNull();
            status = args.ParseBool("-s", "--status");
            wait = args.ParseArgIncludingLast("-w", "--wait").ParseIntOrNull();
            help = args.ParseBool("-h", "--help");
            verbose = args.ParseBool("-v", "--verbose");
            filename = args.LastOrDefault();

            // Early-out with the help message if we have bad args
            if (help)
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
            if (string.IsNullOrWhiteSpace(id))
                UploadTemplate();
            else if (status && !statusValue.HasValue)
                CheckRenderStatus();
            else
                Render();

            PrintGoodbye();
        }

        static void CheckRenderStatus()
        {
            svc.GetRenderStatus(id, statusValue, wait);
        }

        static void Render()
        {
            Render render = new Render();
            render.Source = string.Format("TEMPLATE {0}", id);

            if (!string.IsNullOrWhiteSpace(filename) && File.Exists(filename))
                render.Variables = JsonConvert.DeserializeObject<IDictionary<string, string>>(File.ReadAllText(filename));

            Console.WriteLine(svc.Render(render, statusValue, wait));
        }

        static void UploadTemplate()
        {
            Console.WriteLine(svc.Add(JsonConvert.DeserializeObject<Template>(File.ReadAllText(filename))));
        }
    }
}
