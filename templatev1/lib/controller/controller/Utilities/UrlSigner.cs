using System;
using System.Security.Cryptography;
using System.Text;

namespace controller.Utilities
{
    public class UrlSigner
    {
        private byte[] _secretBytes;

        public UrlSigner(string secret)
        {
            // secret is the URL signing secret, base64 encoded
            _secretBytes = Convert.FromBase64String(secret);
        }

        public string Sign(string url)
        {
            var uri = new Uri(url);
            var signature = GetSignature(uri.AbsolutePath + "?" + uri.Query);
            return url + "&signature=" + signature;
        }

        internal string GetSignature(string input)
        {
            var algorithm = new HMACSHA1(_secretBytes);
            var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(input));
            return Convert.ToBase64String(hash).Replace('+', '-').Replace('/', '_');
        }
    }
}