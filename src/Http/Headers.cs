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

namespace VeryLargeBits.Http
{
    internal static class Headers
    {
        /// <summary>Required on all API calls - see documentation for
        /// the steps to generate the header value.</summary>        
        public const string Authorization = "Authorization";

        /// <summary>If set, rate rejection will be based only on the requests that
        /// share this value.</summary>
        public const string RejectRateGroup = "X-Reject-RateGroup";

        /// <summary>If set, defines the number of allowed requests during a rejection
        /// period. This value will be evaluated individually and can be different for
        /// each request.</summary>        
        public const string RejectRateLimit = "X-Reject-RateLimit";

        /// <summary>If set, specifies the number of seconds used as the time-basis
        /// during rate rejection.</summary>
        /// <remarks>Default value is 3600 (One hour).</remarks>
        public const string RejectRatePer = "X-Reject-RatePer";

        /// <summary>If set on a request, specifies the minimum number of requests
        /// which must remain after this request.</summary>
        public const string RejectRateRemaining = "X-Reject-RateRemaining";

        /// <summary>If set, defines a value in US dollars above which this request
        /// will be rejected.</summary>
        public const string RejectUSD = "X-Reject-USD";

        /// <summary>If present, the http connection will be kept alive
        /// until the X-KeepAliveUntil (or final) status has been reached.</summary>
        public const string Wait = "X-Wait";

        /// <summary>If set, controls how long in whole seconds the http
        /// connection will be kept alive while waiting for a status.</summary>
        public const string WaitFor = "X-Wait-For";

        /// <summary>If set, the http connection will be kept alive until
        /// this status has been reached. Valid values vary per api method.</summary>
        public const string WaitUntil = "X-Wait-Until";
    }
}
