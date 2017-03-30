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
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace VeryLargeBits.Schema
{
    public abstract class Request
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ColorStandard
        {
            BT601,
            BT709,
        }

        public class H264Profile : VideoProfile
        {
            public H264Profile()
            {
                Type = "H264";
            }
        }

        public class HevcProfile : VideoProfile
        {
            public HevcProfile()
            {
                Type = "HEVC";
            }
        }

        public class GifProfile : VideoProfile
        {
            public GifProfile()
            {
                Type = "GIF";
            }
        }

        public class JpegProfile : ProfileBase
        {
            public JpegProfile()
            {
                Type = "JPEG";
                Quality = 0.8D;
            }

            [JsonProperty("quality")]
            public double Quality { get; set; }
        }

        public abstract class JsonTypeBase
        {
            [JsonProperty("type")]
            protected string Type;
        }

        public abstract class ProfileBase : JsonTypeBase
        {
        }

        protected class ProfileConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(ProfileBase);
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
                    case "H264":
                        return json.ToObject<H264Profile>(serializer);

                    case "HEVC":
                        return json.ToObject<HevcProfile>(serializer);

                    case "GIF":
                        return json.ToObject<GifProfile>(serializer);

                    case "JPEG":
                        return json.ToObject<JpegProfile>(serializer);

                    default:
                        throw new NotImplementedException();
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        public abstract class VideoProfile : ProfileBase
        {
            [JsonProperty("bit_rate")]
            public long BitRate { get; set; }

            [JsonProperty("color")]
            public ColorStandard? Color { get; set; }

            [JsonProperty("const_bit_rate")]
            public bool? ConstantBitRate { get; set; }

            [JsonProperty("const_gop")]
            public bool? ConstantGroupOfPictures { get; set; }

            [JsonProperty("gop")]
            public long? GroupOfPictures { get; set; }

            [JsonProperty("max_b_frames")]
            public long? MaxBFrames { get; set; }

            [JsonProperty("pixel_aspect_ratio")]
            public double? PixelAspectRatio { get; set; }
        }
    }
}
