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

using ChristianEtter;
using JavaScience;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using VeryLargeBits.Extensions;
using VeryLargeBits.Http;
using VeryLargeBits.Properties;
using VeryLargeBits.Schema;

namespace VeryLargeBits
{
    public class Client
    {
        // Fields
        private Func<Tuple<HashAlgorithm, RSACryptoServiceProvider>> cryptoProvider;
        private string email, password;

        #region CTORs
        /// <summary>Creates a client using the PKCS #8 (.PEM) private signing key
        /// file specified in the app.config.</summary>
        /// <param name="apiKey">Optional when a value is specified in app.config.</param>
        public static Client Default(string apiKey = null)
        {
            return FromPem8File(Settings.Default.PrivateSigningKeyFilename, apiKey ?? Settings.Default.ApiKey);
        }

        /// <summary>Main ctor</summary>
        /// <param name="cryptoProvider">A factory of one-time-use crypto objects.</param>
        /// <param name="apiKey">Optional when a value is specified in app.config.</param>
        public Client(Func<Tuple<HashAlgorithm, RSACryptoServiceProvider>> cryptoProvider, string apiKey = null)
        {
            ApiKey = apiKey;
            this.cryptoProvider = cryptoProvider;
        }

        /// <summary>Easy-Access ctor</summary>
        /// <param name="email">The email address used to sign in to dashboard.verylargebits.com</param>
        /// <param name="password">Required</param>
        public Client(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
        #endregion

        #region Properties
        public string ApiKey { get; set; }
        #endregion

        #region Public API Methods
        /// <summary>Adds a new asset file to the Very Large Bits system.</summary>
        /// <param name="body">Asset data, optionally specifying that additional
        /// patches will contain the completed data.</param>
        /// <returns>The id of the new asset.</returns>
        public string Add(Asset body, AssetStatus? waitUntil = null, int? waitFor = null)
        {
            var request = AddAuthorization(Post("/asset"), body);
            AddHeaders(request, waitUntil, waitFor);

            return request.ExpectOK<Id>().Value;
        }
        
        /// <summary>Adds a new video template to the Very Large Bits system.</summary>
        /// <returns>The id of the new template.</returns>
        public string Add(Template body)
        {
            return AddAuthorization(Post("/template"), body).ExpectOK<Id>().Value;
        }

        /// <summary>Used to find out if Very Large Bits already has a copy of some
        /// asset file that you have.</summary>
        /// <param name="hash">The SHA-1 hash value of your asset file.</param>
        /// <returns>The id Very Large Bits has for hash, or null.</returns>
        public string GetAssetIdByHash(byte[] hash)
        {
            return AddAuthorization(Get("/assets/" + hash.Base64())).ExpectOK<Id>().Value;
        }

        /// <summary>Used to determine what state a Very Large Bits has for
        /// a given asset.</summary>
        /// <param name="id">The asset identifier.</param>
        /// <returns>The status Very Large Bits has for assetId, or null.</returns>
        public AssetStatus GetAssetStatus(string assetId, AssetStatus? waitUntil = null, int? waitFor = null)
        {
            var request = AddAuthorization(Get("/asset/" + assetId + "/status"));
            AddHeaders(request, waitUntil, waitFor);

            return request.ExpectOK<Status<AssetStatus>>().StatusValue;
        }

        /// <summary>Used to determine what state a Very Large Bits has for
        /// a given render.</summary>
        /// <param name="id">The render identifier.</param>
        /// <returns>The status Very Large Bits has for renderId, or null.</returns>
        public RenderStatus GetRenderStatus(string renderId, RenderStatus? waitUntil = null, int? waitFor = null)
        {
            var request = AddAuthorization(Get("/render/" + renderId + "/status"));
            AddHeaders(request, waitUntil, waitFor);

            return request.ExpectOK<Status<RenderStatus>>().StatusValue;
        }

        /// <summary>Used to send additional data for large assets.</summary>
        /// <param name="assetId">The id of the asset to patch.</param>
        /// <param name="patchIndex">The index of this data patch, where zero is the index
        /// of the data sent using the Add() method. Patch sizes can vary or be zero.</param>
        /// <param name="body">JSON-Formatted body. Note that this method is slightly
        /// less efficient than the octet-stream version, below.</param>
        public void Patch(string assetId, int patchIndex, Patch body, AssetStatus? waitUntil = null, int? waitFor = null)
        {
            var request = AddAuthorization(Patch("/asset/" + assetId + "/" + patchIndex, MimeTypes.Json), body);
            AddHeaders(request, waitUntil, waitFor);
            request.ExpectOK<Response>();
        }

        /// <summary>Used to send additional data for large assets.</summary>
        /// <param name="assetId">The id of the asset to patch.</param>
        /// <param name="patchIndex">The index of this data patch, where zero is the index
        /// of the data sent using the Add() method. Patch sizes can vary or be zero.</param>
        /// <param name="body">Raw asset data.</param>
        public void Patch(string assetId, int patchIndex, byte[] body, AssetStatus? waitUntil = null, int? waitFor = null)
        {
            var request = AddAuthorization(Patch("/asset/" + assetId + "/" + patchIndex), body);
            AddHeaders(request, waitUntil, waitFor);
            request.ExpectOK<Response>();
        }

        /// <summary>Submits a new rendering request.</summary>
        /// <returns>The id of the job.</returns>
        public string Render(Render body, RenderStatus? waitUntil = null, int? waitFor = null)
        {
            var request = AddAuthorization(Post("/render"), body);
            AddHeaders(request, waitUntil, waitFor);

            return request.ExpectOK<Id>().Value;
        }
        #endregion

        #region Api Support and Helper Methods
        static void AddHeaders(HttpWebRequest request, AssetStatus? waitUntil = null, int? waitFor = null)
        {
            if (null != waitUntil)
                request.Headers[Headers.WaitUntil] = Enum.GetName(typeof(AssetStatus), waitUntil.Value);

            if (null != waitFor)
                request.Headers[Headers.WaitFor] = waitFor.ToString();
        }

        static void AddHeaders(HttpWebRequest request, RenderStatus? waitUntil = null, int? waitFor = null)
        {
            if (null != waitUntil)
                request.Headers[Headers.WaitUntil] = Enum.GetName(typeof(RenderStatus), waitUntil.Value);

            if (null != waitFor)
                request.Headers[Headers.WaitFor] = waitFor.ToString();
        }

        static HttpWebRequest Create(string method, string relativeUrl)
        {
            var api = new Uri(Settings.Default.ApiUrl, UriKind.Absolute);
            var uri = new Uri(api, relativeUrl);
            var request = (HttpWebRequest) WebRequest.Create(uri);

            request.Method = method;                     

            return request;
        }        

        static HttpWebRequest Get(string relativeUrl)
        {
            return Create("GET", relativeUrl);
        }

        static HttpWebRequest Post(string relativeUrl)
        {
            var request = Create("POST", relativeUrl);
            request.ContentType = MimeTypes.Json;

            return request;
        }

        static HttpWebRequest Patch(string relativeUrl, string mimeType = MimeTypes.OctetStream)
        {
            var request = Create("PATCH", relativeUrl);
            request.ContentType = mimeType;

            return request;
        }

        HttpWebRequest AddAuthorization(HttpWebRequest request, object body = null)
        {
            if (!string.IsNullOrEmpty(ApiKey))
                return AddSignatureAuthorization(request, body);
            else
                return AddBasicAuthorization(request, body);
        }

        HttpWebRequest AddBasicAuthorization(HttpWebRequest request, object body)
        {
            // Add the standard authorization header with email address and password
            var token = string.Format("{0}:{1}", email, password);
            request.Headers[Headers.Authorization] = string.Format("Basic {0}", Encoding.UTF8.GetBytes(token).Base64());

            if (body is byte[])
            {
                request.GetRequestStream().Write((byte[])body);
            }
            else if (null != body)
            {
                // The body is JSON
                string json = JsonConvert.SerializeObject(body, Formatting.None, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
                request.GetRequestStream().Write(json.UTF8());
            }

            return request;
        }

        HttpWebRequest AddSignatureAuthorization(HttpWebRequest request, object body)
        {
            // Figure out which SHA algorithm has been specified
            var crypto = cryptoProvider();
            string hashAlgorithm;
            if (crypto.Item1 is SHA256)
                hashAlgorithm = "SHA256";
            else if (crypto.Item1 is SHA384)
                hashAlgorithm = "SHA384";
            else if (crypto.Item1 is SHA512)
                hashAlgorithm = "SHA512";
            else
                throw new NotSupportedException("Unsupported hash algorithm provided.");

            // The signing document consists of METHOD+PATH/QUERY+BODY
            var documentBuilder = new StringBuilder();
            documentBuilder.Append(request.Method);
            documentBuilder.Append(request.RequestUri.PathAndQuery);

            // ... add on the BODY part
            byte[] document;
            if (null == body)
                // No body (GET)
                document = documentBuilder.UTF8();
            else if (body is byte[])
            {
                var utf8Bytes = documentBuilder.UTF8();
                var bodyBytes = (byte[])body;

                // The body is a byte stream
                document = new byte[utf8Bytes.Length + bodyBytes.Length];
                Array.Copy(utf8Bytes, document, utf8Bytes.Length);
                Array.Copy(bodyBytes, 0, document, utf8Bytes.Length, bodyBytes.Length);
                request.GetRequestStream().Write(bodyBytes);
            }
            else
            {
                // The body is JSON
                string json = JsonConvert.SerializeObject(body, Formatting.None, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
                documentBuilder.Append(json);
                document = documentBuilder.UTF8();
                request.GetRequestStream().Write(json.UTF8());
            }

            // Add the standard authorization header with the computed signature
            request.Headers[Headers.Authorization] = string.Format("Signature {0}:{1}:{2}", ApiKey, hashAlgorithm, crypto.Item2.SignData(document, crypto.Item1).Base64());

            crypto.Item1.Dispose();
            crypto.Item2.Dispose();

            return request;
        }
        #endregion

        #region Private Key (PKCS #7 .PEM) Support
        public static Client FromPem7File(string filename, string apiKey = null)
        {
            return FromPem7(File.ReadAllText(filename), apiKey);
        }

        public static Client FromPem7(string pem, string apiKey = null)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.LoadPrivateKeyPEM(pem);

            return new Client(delegate
            {
                return new Tuple<HashAlgorithm, RSACryptoServiceProvider>(new SHA256CryptoServiceProvider(), rsa);
            }, apiKey);
        }
        #endregion

        #region Private Key (PKCS #8 .PEM) Support
        public static Client FromPem8File(string filename, string apiKey = null)
        {
            return FromPem8(File.ReadAllText(filename), apiKey);
        }

        public static Client FromPem8(string pem, string apiKey = null)
        {
            byte[] privateKeyInfo;
            if (pem.StartsWith(opensslkey.pemp8header) && pem.EndsWith(opensslkey.pemp8footer))
                privateKeyInfo = opensslkey.DecodePrivateKeyInfo(opensslkey.DecodePkcs8PrivateKey(pem));
            else
                throw new Exception("Not a PEM PKCS #8");            

            return new Client(delegate
            {
                return new Tuple<HashAlgorithm, RSACryptoServiceProvider>(new SHA256CryptoServiceProvider(), opensslkey.DecodeRSAPrivateKey(privateKeyInfo));
            }, apiKey);
        }
        #endregion
    }
}
