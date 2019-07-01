using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clickandgo.Models
{
    public class Driver
    {
        public MongoDB.Bson.ObjectId _id { get; set; }

        public string PrimaryId { get; set; }

        public string VehicleID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime DateIssued { get; set; }

        public DateTime DateExpired { get; set; }

        public string Gender { get; set; }

        public string Class { get; set; }

        public string Trn { get; set; }

        public string Collectorate { get; set; }

        public Address Address { get; set; }

        public DateTime DOB { get; set; }

        public string LicenseToDrive { get; set; }

        public string PPV { get; set; } //23

        public string PoliceRecordNumber { get; set; }

        public string TimePoliceRecordIssue { get; set; }

        public string InformationAccurate { get; set; }

        public string Terms { get; set; }

        public string status { get; set; }
    }
}
