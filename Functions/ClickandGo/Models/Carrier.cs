using System;
using System.Collections.Generic;
using System.Text;

namespace ClickandGo.Models
{
    public class Carrier
    {
        public string id { get; set; }

        public string RiderId { get; set; }

        public string DriverId { get; set; }

        public Cords Origin { get; set; }

        public Cords Destination { get; set; }

        public string Distance { get; set; }

        public string Duration { get; set; }

        public double Fair { get; set; }
    }
}
