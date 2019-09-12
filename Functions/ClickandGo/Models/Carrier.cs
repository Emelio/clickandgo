using System;
using System.Collections.Generic;
using System.Text;

namespace ClickandGo.Models
{
    class Carrier
    {
        public string id { get; set; }

        public string RiderId { get; set; }

        public string DriverId { get; set; }

        public Cords Origin { get; set; }

        public Cords Destination { get; set; }

        public double Fair { get; set; }
    }
}
