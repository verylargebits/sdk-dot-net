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
using System.Collections.Generic;

namespace VeryLargeBits.Schema
{
    public abstract class Request
    {
        public class Http : UrlBase
        {
            public Http()
            {
                Type = "HTTP";
            }

            [JsonProperty("headers")]
            public IDictionary<string, string> Headers { get; set; }

            [JsonProperty("verb")]
            public string Verb { get; set; }
        }

        public class Metered : StorageBase
        {
            public Metered()
            {
                Type = "METERED";
            }

            [JsonProperty("maxCount")]
            public ulong? MaxCount { get; set; }

            [JsonProperty("maxDays")]
            public ulong? MaxDays { get; set; }

            [JsonProperty("maxSpend")]
            public string MaxSpend { get; set; }
        }

        public abstract class RetryBase : StorageBase
        {
            [JsonProperty("retryCount")]
            public int RetryCount { get; set; }

            [JsonProperty("retryDelay")]
            public int RetryDelay { get; set; }
        }

        public class S3 : UrlBase
        {
            public S3()
            {
                Type = "S3";
            }

            [JsonProperty("password")]
            public string password { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }
        }

        public abstract class StorageBase
        {
            [JsonProperty("type")]
            protected string Type;
        }

        public class Transcode
        {
            [JsonProperty("endFrame")]
            public int? EndFrame;

            [JsonProperty("endSeconds")]
            public double? endSeconds;

            [JsonProperty("frameCount")]
            public int? FrameCount;

            [JsonProperty("height")]
            public int? Height;

            [JsonProperty("profile")]
            public Profile Profile;

            [JsonProperty("profileId")]
            public string ProfileId;

            [JsonProperty("startFrame")]
            public int? StartFrame;

            [JsonProperty("startSeconds")]
            public double? StartSeconds;

            [JsonProperty("totalSeconds")]
            public double? TotalSeconds;

            [JsonProperty("width")]
            public int? Width;
        }

        public abstract class UrlBase : RetryBase
        {
            [JsonProperty("url")]
            public string url { get; set; }
        }

        [JsonProperty("storage")]
        public StorageBase Storage { get; set; }

        [JsonProperty("transcodes")]
        public IEnumerable<Transcode> Transcodes { get; set; }
    }
}
