using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Starlight.Core
{
    /// <summary>
    /// Represents a JSON Web Token.
    /// Check: https://jwt.io/
    /// </summary>
    public class Jwt
    {
        #region Properties
        /// <summary>
        /// Header Daten des Tokens.
        /// </summary>
        public IDictionary<string, string> Header { get; private set; }

        /// <summary>
        /// Payload Daten des Tokens.
        /// </summary>
        public IDictionary<string, string> Payload { get; private set; }

        /// <summary>
        /// Signatur des Tokens.
        /// </summary>
        public IEnumerable<byte> Signature { get; private set; }
        #endregion

        #region Ctor/dtor
        /// <summary>
        /// Creates a new instance from the given token.
        /// </summary>
        /// <param name="token">The token string, consisting of 3 Base64-URL-encoded strings separated with '.'.</param>
        public Jwt(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                Debug.Fail("Token string must not be null or empty.");
                throw new ArgumentNullException(nameof(token));
            }

            var parts = token.Split(new char[] { '.' });
            if (parts.Length != 3)
            {
                var msg = "Invalid token string. Token must consist of 3 strings separated by '.'.";
                Debug.Fail(msg);
                throw new ArgumentException(msg);
            }

            // header
            var header = Encoding.Default.GetString(Base64UrlDecode(parts[0]));
            this.Header = Json.ReadFromString<IDictionary<string, string>>(header);

            // payload
            var data = Encoding.Default.GetString(Base64UrlDecode(parts[1]));
            this.Payload = Payload = Json.ReadFromString<IDictionary<string, string>>(data);

            // signatur
            this.Signature = Base64UrlDecode(parts[2]);
        }
        #endregion

        //
        // ripped off of: https://stackoverflow.com/questions/1228701/code-for-decoding-encoding-a-modified-base64-url
        //
        private static byte[] Base64UrlDecode(string arg)
        {
            if (string.IsNullOrEmpty(arg))
            {
                Debug.Fail("Argument must not be null or empty!");
                throw new ArgumentNullException(nameof(arg));
            }

            // 62nd char of encoding: '-' => '+'
            // 63rd char of encoding: '_' => '/'
            var s = arg.Replace('-', '+').Replace('_', '/');

            // Pad with trailing '='s
            switch (s.Length % 4)
            {
                case 0:
                    break;
                case 2:
                    s += "==";
                    break;
                case 3:
                    s += "=";
                    break;
                default:
                    throw new Exception("Illegal base64url string!");
            }
            return Convert.FromBase64String(s);
        }
    }
}
