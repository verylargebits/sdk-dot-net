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
using System.IO;
using System.Net;
using System.Text;
using VeryLargeBits.Schema;

namespace VeryLargeBits.Extensions
{
    internal static class HttpWebRequestExtensions
    {
        public static T ExpectOK<T>(this HttpWebRequest instance)
            where T : Response
        {
            // Throw an exception if the HTTP status isn't 200 OK
            var response = (HttpWebResponse)instance.GetResponse();
            if (HttpStatusCode.OK != response.StatusCode)
                throw new Exception(Enum.GetName(typeof(HttpStatusCode), response.StatusCode));

            // All Very Large Bits API responses are JSON
            return JsonConvert.DeserializeObject<T>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());
        }
    }
}
