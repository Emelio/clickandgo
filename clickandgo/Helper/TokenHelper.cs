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

            byte[] data = Convert.FromBase64String(tokenString[1]);
            string decodedString = Encoding.UTF8.GetString(data);
            JsonJwtPayload jsonJwtPayload = JsonConvert.DeserializeObject<JsonJwtPayload>(decodedString);

            return jsonJwtPayload.nameid;
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
