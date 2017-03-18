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
    public class Template : Request
    {
        public class AudioLayer : FileLayerBase
        {
            public AudioLayer()
            {
                Type = "AUDIO";
            }
        }

        public enum BlendMode
        {
            [JsonProperty("ADD")]
            Add,

            [JsonProperty("ALPHA_ADD")]
            AlphaAdd,

            [JsonProperty("COLOR")]
            Color,

            [JsonProperty("COLOR_BURN")]
            ColorBurn,

            [JsonProperty("COLOR_DODGE")]
            ColorDodge,

            [JsonProperty("DARKEN")]
            Darken,

            [JsonProperty("DARKER_COLOR")]
            DarkerColor,

            [JsonProperty("DIFFERENCE")]
            Difference,

            [JsonProperty("DIVIDE")]
            Divide,

            [JsonProperty("EXCLUSION")]
            Exclusion,

            [JsonProperty("HARD_LIGHT")]
            HardLight,

            [JsonProperty("HARD_MIX")]
            HardMix,

            [JsonProperty("HUE")]
            Hue,

            [JsonProperty("LIGHTEN")]
            Lighten,

            [JsonProperty("LIGHTER_COLOR")]
            LighterColor,

            [JsonProperty("LINEAR_COLOR")]
            LinearColor,

            [JsonProperty("LINEAR_LIGHT")]
            LinearLight,

            [JsonProperty("LUMINOSITY")]
            Luminosity,

            [JsonProperty("MULTIPLY")]
            Multiply,

            [JsonProperty("NORMAL")]
            Normal,

            [JsonProperty("OVERLAY")]
            Overlay,

            [JsonProperty("PIN_LIGHT")]
            PinLight,

            [JsonProperty("SATURATION")]
            Saturation,

            [JsonProperty("SCREEN")]
            Screen,

            [JsonProperty("SILHOUETTE_ALPHA")]
            SilhouetteAlpha,

            [JsonProperty("SILHOUETTE_LUMA")]
            SilhouetteLuma,

            [JsonProperty("SOFT_LIGHT")]
            SoftLight,

            [JsonProperty("STENCIL_ALPHA")]
            StencilAlpha,

            [JsonProperty("STENCIL_LUMA")]
            StencilLuma,

            [JsonProperty("SUBTRACT")]
            Subtract,

            [JsonProperty("VIVID_LIGHT")]
            VividLight,
        }

        public abstract class FileLayerBase : LayerBase
        {
            [JsonProperty("source")]
            public string Source = null;
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
            }

            [JsonProperty("height")]
            public int Height;

            [JsonProperty("width")]
            public int Width;

            [JsonProperty("layers")]
            public List<LayerBase> Layers = new List<LayerBase>();
        }

        public class ImageLayer : FileLayerBase
        {
            public ImageLayer()
            {
                Type = "IMAGE";
            }

            [JsonProperty("blend")]
            public BlendMode Blend = BlendMode.Normal;

            [JsonProperty("matte")]
            public string Matte = "BETTER";
        }

        public abstract class JsonTypeBase
        {
            [JsonProperty("type")]
            protected string Type;
        }

        public abstract class LayerBase : JsonTypeBase
        {
            [JsonProperty("id")]
            public long? Id = null;

            [JsonProperty("variable")]
            public string Variable = null;
        }

        public abstract class PropertyBase : JsonTypeBase
        {
            [JsonProperty("id")]
            public long Id;
        }

        public class TransformProperty : PropertyBase
        {
            public TransformProperty()
            {
                Type = "TRANSFORM";
            }

            [JsonProperty("offset")]
            public string Offset = null;

            [JsonProperty("opacity")]
            public double? Opacity = null;

            [JsonProperty("position")]
            public string Position = null;

            [JsonProperty("rotation")]
            public string Rotation = null;

            [JsonProperty("scale")]
            public string Scale = null;
        }

        public class VideoLayer : FileLayerBase
        {
            public VideoLayer()
            {
                Type = "VIDEO";
            }
        }

        [JsonProperty("name")]
        public string Name = "Default";

        [JsonProperty("background")]
        public string Background = "#ffffff";

        [JsonProperty("frameRate")]
        public double FrameRate = 30;

        [JsonProperty("frames")]
        public IList<Frame> Frames = new List<Frame>();

        [JsonProperty("profile")]
        public Profile Profile;

        [JsonProperty("profileId")]
        public string ProfileId;
    }
}
