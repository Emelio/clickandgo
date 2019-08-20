using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clickandgo.Models
{
    public class Users
    {
        public MongoDB.Bson.ObjectId _id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string MiddleName { get; set; }

        public Address Address { get; set; }

        public double Trn { get; set; }

        public string PoliceRecordNumber { get; set; }

        public string Gender { get; set; }

        public DateTime DOB { get; set; }

        public string Email { get; set; }

        public Contact Contact { get; set; }

        public string Type { get; set; }

        public string Stage { get; set; }

        public string Verified { get; set; }

        public string VerificationCode { get; set; }

        public string ApprovalStatus { get; set; }="hold";
    }
}
