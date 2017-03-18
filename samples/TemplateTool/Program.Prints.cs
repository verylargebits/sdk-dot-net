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

namespace VeryLargeBits.TemplateTool
{
    static partial class Program
    {
        static void PrintHelp()
        {
            Console.WriteLine(@"Usage: vlb [OPTION]... [FILE]
Adds and renders templates using the Very Large Bits system. Examples:
  vlb template.json
  vlb -v template.json
  vlba --render 6nnrqkpbffq8ke6y7rh6trccz5 vars.json
  vlba --key fyn615m07rbpfmydvk3re6wwgq --secret c:\\mykeyfile.pkcs8 template.json

Also blocks until a specific status is reached. Example:
  vlba -s DONE -r 6nnrqkpbffq8ke6y7rh6trccz5 vars.json

Security OPTIONs when using API Key authentication:
  -k or --key      Override the app.config API key value.
  -s or --secret   Override the app.config private key filename value.

Security OPTIONs when using basic API authentication:
  -e or --email    The email address used to sign in to dashboard.verylargebits.com.
  -p or --password Required if email is specificied.

Template OPTIONs:
  [FILE]           A JSON-formatted template file conforming to the Very Large Bits
                   standards.

Render OPTIONs:
  [FILE]           A JSON-formatted dictionary file which specifies
                   variable-replacement operations, if any.
  -r or --render   The ID of the template to render with variable replacements or
                   the ID of the render to check or wait upon for status.
  -s or --status   Checks the status if none provided or waits until the
                   provided status is reached. Only valid value is DONE.
  -w or --wait     The number of seconds to wait if a status value is provided with -s.
                   Default is 600 (10 minutes).

Other OPTIONs:
  -h or --help     Print this message.  
  -v or --verbose  Enable detailed output.");
        }

        static void PrintGoodbye()
        {
        }
    }
}