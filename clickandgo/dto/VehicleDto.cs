using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clickandgo.dto
{
    public class VehicleDto
    {
        public string vehicleId { get; set; }
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

        public DateTime? RegIssue { get; set; } = null;

        public DateTime? RegExpire { get; set; } = null;

        public DateTime? FitIssue { get; set; } = null;

        public DateTime? FitExpiry { get; set; } = null;

        public string InsuranceName { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime? PolicyIssueDate { get; set; } = null;

        public DateTime? PolicyExpiryDate { get; set; } = null;
    }
}
