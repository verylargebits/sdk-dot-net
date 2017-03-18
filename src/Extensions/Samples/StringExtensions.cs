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
using VeryLargeBits.Schema;

namespace VeryLargeBits.Extensions.Samples
{
    public static class StringExtensions
    {
        public static string ParseArg(this string[] args, params string[] tags)
        {
            foreach (var tag in tags)
            {
                var index = Array.IndexOf(args, tag);
                if (0 <= index && index < args.Length - 2)
                    return args[index + 1];
            }

            return null;
        }

        public static string ParseArgIncludingLast(this string[] args, params string[] tags)
        {
            foreach (var tag in tags)
            {
                var index = Array.IndexOf(args, tag);
                if (0 <= index && index < args.Length - 1)
                    return args[index + 1];
            }

            return null;
        }

        public static AssetStatus? ParseAssetStatusOrNull(this string instance)
        {
            if (null != instance && Enum.IsDefined(typeof(AssetStatus), instance))
                return (AssetStatus)Enum.Parse(typeof(AssetStatus), instance);
            else
                return null;
        }

        public static TemplateStatus? ParseTemplateStatusOrNull(this string instance)
        {
            if (null != instance && Enum.IsDefined(typeof(TemplateStatus), instance))
                return (TemplateStatus)Enum.Parse(typeof(TemplateStatus), instance);
            else
                return null;
        }

        public static RenderStatus? ParseRenderStatusOrNull(this string instance)
        {
            if (null != instance && Enum.IsDefined(typeof(RenderStatus), instance))
                return (RenderStatus)Enum.Parse(typeof(RenderStatus), instance);
            else
                return null;
        }

        public static bool ParseBool(this string[] args, params string[] tags)
        {
            foreach (var tag in tags)
            {
                var index = Array.IndexOf(args, tag);
                if (0 <= index && index < args.Length - 1)
                    return true;
            }

            return false;
        }

        public static int ParseByteSize(this string instance)
        {
            instance = instance.Replace(" ", "").ToLower();
            var multiplier = 1;

            if (instance.EndsWith("mb"))
                multiplier = 1024 * 1024;
            else if (instance.EndsWith("kb"))
                multiplier = 1024;

            if (1 != multiplier)
                instance = instance.Substring(0, instance.Length - 2);

            return int.Parse(instance) * multiplier;
        }

        public static int? ParseIntOrNull(this string instance)
        {
            int result;
            if (int.TryParse(instance, out result))
                return result;
            else
                return null;
        }
    }
}
