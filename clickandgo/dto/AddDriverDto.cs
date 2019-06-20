using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clickandgo.dto
{
    public class AddDriverDto
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PrimaryId { get; set; }

        public DateTime DateIssued { get; set; }

        public DateTime DateExpired { get; set; }

        public string Gender { get; set; }

        public string Class { get; set; }

        public string Trn { get; set; }

        public string Collectorate { get; set; }

        public string StreetAddress { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string Parish { get; set; }

        public string Country { get; set; }

        public DateTime DOB { get; set; }

        public string LicenseToDrive { get; set; }

        public string PPV { get; set; }

        public string PoliceRecordNumber { get; set; }

        public string TimePoliceRecordIssue { get; set; }

        public string InformationAccurate { get; set; }

        public string Terms { get; set; }


    }
}
