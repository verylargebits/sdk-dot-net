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

namespace VeryLargeBits.Schema
{
    public enum AssetStatus
    {
        /// <summary>The hash provided with the initial asset POST does not
        /// match the calculated hash of the given patches.</summary>
        [JsonProperty("BAD_HASH")]
        BadHash,

        /// <summary>Unexpected system error. Please contact support.</summary>
        [JsonProperty("ERROR")]
        Error,

        /// <summary>The asset is being hashed by Very Large Bits to match against
        /// the provided hash value.</summary>
        [JsonProperty("HASHING")]
        Hashing,

        /// <summary>The asset is waiting for remaining data patches to be sent.</summary>
        [JsonProperty("PATCHING")]
        Patching,

        /// <summary>The asset is ready and usable for rendering purposes.</summary>
        [JsonProperty("USABLE")]
        Usable,
        
        /// <summary>The asset is waiting for the first data patch to be sent.</summary>
        [JsonProperty("WAITING")]
        Waiting,
    }
}
