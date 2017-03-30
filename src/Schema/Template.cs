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
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace VeryLargeBits.Schema
{
    public class Template : Request
    {
        [DataContract,
            JsonConverter(typeof(StringEnumConverter))]
        public enum AudioFormat
        {
            [EnumMember(Value = "MP3")]
            Mp3,

            [JsonProperty("WAV")]
            Wav,
        }

        public class AudioLayer : FileLayerBase
        {
            public AudioLayer()
            {
                Type = "AUDIO";
            }

            [JsonProperty("format")]
            public AudioFormat Format { get; set; }
        }

        [DataContract,
            JsonConverter(typeof(StringEnumConverter))]
        public enum BlendMode
        {
            [EnumMember(Value = "ADD")]
            Add,

            [EnumMember(Value = "ALPHA_ADD")]
            AlphaAdd,

            [EnumMember(Value = "COLOR")]
            Color,

            [EnumMember(Value = "COLOR_BURN")]
            ColorBurn,

            [EnumMember(Value = "COLOR_DODGE")]
            ColorDodge,

            [EnumMember(Value = "DARKEN")]
            Darken,

            [EnumMember(Value = "DARKER_COLOR")]
            DarkerColor,

            [EnumMember(Value = "DIFFERENCE")]
            Difference,

            [EnumMember(Value = "DIVIDE")]
            Divide,

            [EnumMember(Value = "EXCLUSION")]
            Exclusion,

            [EnumMember(Value = "HARD_LIGHT")]
            HardLight,

            [EnumMember(Value = "HARD_MIX")]
            HardMix,

            [EnumMember(Value = "HUE")]
            Hue,

            [EnumMember(Value = "LIGHTEN")]
            Lighten,

            [EnumMember(Value = "LIGHTER_COLOR")]
            LighterColor,

            [EnumMember(Value = "LINEAR_COLOR")]
            LinearColor,

            [EnumMember(Value = "LINEAR_LIGHT")]
            LinearLight,

            [EnumMember(Value = "LUMINOSITY")]
            Luminosity,

            [EnumMember(Value = "MULTIPLY")]
            Multiply,

            [EnumMember(Value = "NORMAL")]
            Normal,

            [EnumMember(Value = "OVERLAY")]
            Overlay,

            [EnumMember(Value = "PIN_LIGHT")]
            PinLight,

            [EnumMember(Value = "SATURATION")]
            Saturation,

            [EnumMember(Value = "SCREEN")]
            Screen,

            [EnumMember(Value = "SILHOUETTE_ALPHA")]
            SilhouetteAlpha,

            [EnumMember(Value = "SILHOUETTE_LUMA")]
            SilhouetteLuma,

            [EnumMember(Value = "SOFT_LIGHT")]
            SoftLight,

            [EnumMember(Value = "STENCIL_ALPHA")]
            StencilAlpha,

            [EnumMember(Value = "STENCIL_LUMA")]
            StencilLuma,

            [EnumMember(Value = "SUBTRACT")]
            Subtract,

            [EnumMember(Value = "VIVID_LIGHT")]
            VividLight,
        }

        public class CameraProperty : PropertyBase
        {
            public CameraProperty()
            {
                Type = "CAMERA";
            }

            [JsonProperty("position")]
            public string Position { get; set; }

            [JsonProperty("rotation")]
            public string Rotation { get; set; }

            [JsonProperty("target")]
            public string Target { get; set; }

            [JsonProperty("zoom")]
            public double Zoom { get; set; }
        }

        [DataContract,
            JsonConverter(typeof(StringEnumConverter))]
        public enum ContainerFormat
        {
            [EnumMember(Value = "AVI")]
            Avi,

            [EnumMember(Value = "GIF")]
            Gif,

            [EnumMember(Value = "MOV")]
            Mov,

            [EnumMember(Value = "MP4")]
            Mp4,

            [EnumMember(Value = "MPEG")]
            Mpeg,
        }

        public class CornerPinProperty : PropertyBase
        {
            public CornerPinProperty()
            {
                Type = "CORNER_PIN";
            }

            [JsonProperty("bottom_left")]
            public string BottomLeft { get; set; }

            [JsonProperty("bottom_right")]
            public string BottomRight { get; set; }

            [JsonProperty("top_left")]
            public string TopLeft { get; set; }

            [JsonProperty("top_right")]
            public string TopRight { get; set; }
        }

        public abstract class FileLayerBase : LayerBase
        {
            [JsonProperty("src")]
            public string Source { get; set; }
        }

        public class Frame : List<PropertyBase>
        {
            public Frame(IEnumerable<PropertyBase> properties)
                : base(properties)
            {
            }
        }

        public class GroupLayer : LayerBase
        {
            public GroupLayer()
            {
                Type = "GROUP";
                Blend = BlendMode.Normal;
                Layers = new List<LayerBase>();
            }

            [JsonProperty("blend")]
            public BlendMode Blend { get; set; }

            [JsonProperty("height")]
            public int Height { get; set; }

            [JsonProperty("layers")]
            public List<LayerBase> Layers { get; set; }

            [JsonProperty("matte")]
            public LayerBase Matte { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        [DataContract,
            JsonConverter(typeof(StringEnumConverter))]
        public enum ImageFormat
        {
            [EnumMember(Value = "BMP")]
            Bmp,

            [EnumMember(Value = "DDS")]
            Dds,

            [EnumMember(Value = "EXR")]
            Exr,

            [EnumMember(Value = "HDR")]
            Hdr,

            [EnumMember(Value = "ICO")]
            Ico,

            [EnumMember(Value = "J2K")]
            J2k,

            [EnumMember(Value = "JNG")]
            Jng,

            [EnumMember(Value = "JP2")]
            Jp2,

            [EnumMember(Value = "JPEG")]
            Jpeg,

            [EnumMember(Value = "MNG")]
            Mng,

            [EnumMember(Value = "PNG")]
            Png,

            [EnumMember(Value = "PSD")]
            Psd,

            [EnumMember(Value = "RAW")]
            Raw,

            [EnumMember(Value = "TARGA")]
            Targa,

            [EnumMember(Value = "TIFF")]
            Tiff,
        }

        public class ImageLayer : FileLayerBase
        {
            public ImageLayer()
            {
                Type = "IMAGE";
                Blend = BlendMode.Normal;
            }

            [JsonProperty("blend")]
            public BlendMode Blend { get; set; }

            [JsonProperty("format")]
            public ImageFormat Format { get; set; }

            [JsonProperty("matte")]
            public LayerBase Matte { get; set; }

            [JsonProperty("var")]
            public string Variable { get; set; }
        }

        public abstract class IdTypeBase : JsonTypeBase
        {
            [JsonProperty("id")]
            public long Id { get; set; }
        }

        public abstract class LayerBase : IdTypeBase
        {
        }

        private class LayerConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(LayerBase);
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
                    case "AUDIO":
                        return json.ToObject<AudioLayer>(serializer);

                    case "GROUP":
                        return json.ToObject<GroupLayer>(serializer);

                    case "IMAGE":
                        return json.ToObject<ImageLayer>(serializer);

                    case "VIDEO":
                        return json.ToObject<VideoLayer>(serializer);

                    default:
                        throw new NotImplementedException();
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        [DataContract,
            JsonConverter(typeof(StringEnumConverter))]
        public enum MaskMode
        {
            [EnumMember(Value = "ADD")]
            Add,

            [EnumMember(Value = "DARKEN")]
            Darken,

            [EnumMember(Value = "DIFFERENCE")]
            Difference,

            [EnumMember(Value = "INTERSECT")]
            Intersect,

            [EnumMember(Value = "LIGHTEN")]
            Lighten,

            [EnumMember(Value = "SUBTRACT")]
            Subtract,
        }

        public class MaskProperty : PropertyBase
        {
            public MaskProperty()
            {
                Type = "MASK";
            }

            [JsonProperty("feather")]
            public string Feather { get; set; }

            [JsonProperty("invert")]
            public bool? Invert { get; set; }

            [JsonProperty("mode")]
            public MaskMode Mode { get; set; }

            [JsonProperty("opacity")]
            public double? Opacity { get; set; }

            [JsonProperty("vertices")]
            public string Vertices { get; set; }
        }

        public class OrthoProperty : PropertyBase
        {
            public OrthoProperty()
            {
                Type = "ORTHO";
            }
        }

        public abstract class PropertyBase : IdTypeBase
        {
        }

        private class PropertyConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(PropertyBase);
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
                    case "CAMERA":
                        return json.ToObject<CameraProperty>(serializer);

                    case "CORNER_PIN":
                        return json.ToObject<CornerPinProperty>(serializer);

                    case "MASK":
                        return json.ToObject<MaskProperty>(serializer);

                    case "ORTHO":
                        return json.ToObject<OrthoProperty>(serializer);

                    case "TRANSFORM":
                        return json.ToObject<TransformProperty>(serializer);

                    case "VIDEO":
                        return json.ToObject<VideoProperty>(serializer);

                    default:
                        throw new NotImplementedException();
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        public class TransformProperty : PropertyBase
        {
            public TransformProperty()
            {
                Type = "TRANSFORM";
            }

            [JsonProperty("offset")]
            public string Offset { get; set; }

            [JsonProperty("opacity")]
            public double? Opacity { get; set; }

            [JsonProperty("position")]
            public string Position { get; set; }

            [JsonProperty("rotation")]
            public string Rotation { get; set; }

            [JsonProperty("scale")]
            public string Scale { get; set; }
        }

        public class VideoLayer : FileLayerBase
        {
            public VideoLayer()
            {
                Type = "VIDEO";
                Blend = BlendMode.Normal;
            }

            [JsonProperty("blend")]
            public BlendMode Blend { get; set; }

            [JsonProperty("format")]
            public ContainerFormat Format { get; set; }

            [JsonProperty("matte")]
            public LayerBase Matte { get; set; }

            [JsonProperty("var")]
            public string Variable { get; set; }
        }

        public class VideoProperty : PropertyBase
        {
            public VideoProperty()
            {
                Type = "VIDEO";
            }

            [JsonProperty("position")]
            public string Position;
        }

        // Required converters -> Must be used during deserialization
        public static JsonConverter[] TypeConverters = new JsonConverter[] { new LayerConverter(), new ProfileConverter(), new PropertyConverter() };

        [JsonProperty("name")]
        public string Name = "Default";

        [JsonProperty("background")]
        public string Background = "#ffffff";

        [JsonProperty("frame_rate")]
        public double FrameRate = 30;

        [JsonProperty("frames")]
        public IList<Frame> Frames = new List<Frame>();

        [JsonProperty("height")]
        public int Height;

        [JsonProperty("layers")]
        public IList<LayerBase> Layers = new List<LayerBase>();

        [JsonProperty("profile")]
        public ProfileBase Profile;

        [JsonProperty("profile_id")]
        public string ProfileId;

        [JsonProperty("width")]
        public int Width;
    }
}
