using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clickandgo.Helper
{
    public class TokenHelper
    {
        public string getUserFromToken(string token)
        {
            var tokenString = token.Split(".");



            byte[] data = DecodeUrlBase64(tokenString[1]);
            string decodedString = Encoding.UTF8.GetString(data);
            JsonJwtPayload jsonJwtPayload = JsonConvert.DeserializeObject<JsonJwtPayload>(decodedString);

            return jsonJwtPayload.nameid;
        }

        public static byte[] DecodeUrlBase64(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/').PadRight(4 * ((s.Length + 3) / 4), '=');
            return Convert.FromBase64String(s);
        }
    }

    class JsonJwtPayload
    {
        public string nameid { get; set; }

        public string unique_name { get; set; }

        public double nbf { get; set; }

        public double exp { get; set; }

        public double iat { get; set; }
    }
}
