﻿/* Copyright(c) 2017, Very Large Bits LLC

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
using System.Runtime.Serialization;

namespace VeryLargeBits.Schema
{
    [DataContract,
        JsonConverter(typeof(StringEnumConverter))]
    public enum RenderStatus
    {
        /// <summary>Unexpected system error. Please contact support.</summary>
        [EnumMember(Value = "ERROR")]
        Error,

        /// <summary>The asset is ready and usable for rendering purposes.</summary>
        [EnumMember(Value = "COLLECT_ERROR")]
        CollectError,
        
        /// <summary>The asset is ready and usable for rendering purposes.</summary>
        [EnumMember(Value = "COLLECTING")]
        Collecting,

        /// <summary>The asset is ready and usable for rendering purposes.</summary>
        [EnumMember(Value = "DONE")]
        Done,

        /// <summary>The asset is ready and usable for rendering purposes.</summary>
        [EnumMember(Value = "RENDERING")]
        Rendering,

        /// <summary>The asset is ready and usable for rendering purposes.</summary>
        [EnumMember(Value = "TRANSCODING")]
        Transcoding,

        /// <summary>The asset is ready and usable for rendering purposes.</summary>
        [EnumMember(Value = "TRANSCODE_STORAGE_ERROR")]
        TranscodeStorageError,

        /// <summary>The asset is ready and usable for rendering purposes.</summary>
        [EnumMember(Value = "WAITING")]
        Waiting,
    }
}
