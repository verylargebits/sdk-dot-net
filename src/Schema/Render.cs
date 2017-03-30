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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace VeryLargeBits.Schema
{
    public class Render : Request
    {
        public Render()
        {
            Variables = new Dictionary<string, string>();
        }

        public class HttpStorage : UrlStorageBase
        {
            public HttpStorage()
            {
                Type = "HTTP";
            }

            [JsonProperty("headers")]
            public IDictionary<string, string> Headers { get; set; }

            [JsonProperty("verb")]
            public string Verb { get; set; }
        }

        public class MeteredStorage : StorageBase
        {
            public MeteredStorage()
            {
                Type = "METERED";
            }

            [JsonProperty("max_count")]
            public ulong? MaxCount { get; set; }

            [JsonProperty("max_days")]
            public ulong? MaxDays { get; set; }

            [JsonProperty("max_spend")]
            public string MaxSpend { get; set; }
        }

        public abstract class RetryStorageBase : StorageBase
        {
            [JsonProperty("retry_count")]
            public int RetryCount { get; set; }

            [JsonProperty("retry_delay")]
            public int RetryDelay { get; set; }
        }

        public class S3Storage : UrlStorageBase
        {
            public S3Storage()
            {
                Type = "S3";
            }

            [JsonProperty("password")]
            public string password { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }
        }

        public abstract class StorageBase : JsonTypeBase
        {
        }

        private class StorageConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(StorageBase);
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var json = JObject.Load(reader);
                switch (json["type"].Value<string>())
                {
                    case "HTTP":
                        return json.ToObject<HttpStorage>(serializer);

                    case "METERED":
                        return json.ToObject<MeteredStorage>(serializer);

                    case "S3":
                        return json.ToObject<S3Storage>(serializer);

                    default:
                        throw new NotImplementedException();
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        public class Transcode
        {
            [JsonProperty("end_frame")]
            public int? EndFrame;

            [JsonProperty("end_seconds")]
            public double? endSeconds;

            [JsonProperty("frame_count")]
            public int? FrameCount;

            [JsonProperty("height")]
            public int? Height;

            [JsonProperty("profile")]
            public ProfileBase Profile;

            [JsonProperty("profile_id")]
            public string ProfileId;

            [JsonProperty("start_frame")]
            public int? StartFrame;

            [JsonProperty("start_seconds")]
            public double? StartSeconds;

            [JsonProperty("total_seconds")]
            public double? TotalSeconds;

            [JsonProperty("width")]
            public int? Width;
        }

        public abstract class UrlStorageBase : RetryStorageBase
        {
            [JsonProperty("url")]
            public string url { get; set; }
        }

        // Required converters -> Must be used during deserialization
        public static JsonConverter[] TypeConverters = new JsonConverter[] { new ProfileConverter(), new StorageConverter() };

        [JsonProperty("src")]
        public string Source;

        [JsonProperty("storage")]
        public StorageBase Storage { get; set; }

        [JsonProperty("transcodes")]
        public IEnumerable<Transcode> Transcodes { get; set; }
        
        [JsonProperty("vars")]
        public IDictionary<string, string> Variables { get; set; }
    }
}
