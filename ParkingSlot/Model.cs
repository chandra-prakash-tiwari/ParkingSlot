using System;
using System.Collections.Generic;

namespace ParkingSlot
{
    public class Model
    {
        public string Id { get; set; }

        public string VehicalNumber { get; set; }

        public int Type { get; set; }

        public DateTime EnteryTime { get; set; }

        public DateTime ExitTime { get; set;}

        public Status Status { get; set; }
    }

    public class Database
    {
        public List<Model> Records { get; set; }

        public Database()
        {
            Records = new List<Model>();
        }
    }

    public class ParkingRatesList
    {
        public static List<RateChart> ParkingRates { get; set; }

        public ParkingRatesList()
        {
            ParkingRates = new List<RateChart>();
        }
    }

    public class RateChart
    {
        public int Type { get; set; }

        public float Rate { get; set; }

        public int NoOfSlots { get; set; }
    }
}
