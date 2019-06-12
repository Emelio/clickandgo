using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clickandgo.Models
{
    public class Vehicle
    {
        public MongoDB.Bson.ObjectId _id { get; set; }

        public string OwnerId { get; set; }

        public string VehicleType { get; set; }

        public string Make { get; set; }

        public string Year { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string BodyType { get; set; }

        public string EngineNumber { get; set; }

        public string ChassisNumber { get; set; }

        public string PlateNumber { get; set; }

        public DateTime? RegIssue { get; set; }

        public DateTime? RegExpire { get; set; }

        public DateTime? FitIssue { get; set; }

        public DateTime? FitExpiry { get; set; }

        public string InsuranceName { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime? PolicyIssueDate { get; set; }

        public DateTime? PolicyExpiryDate { get; set; }


    }
}
