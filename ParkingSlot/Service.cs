using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingSlot
{
    public class Service :IService
    {
        private Database Database { get; set; }
        public Service(Database database)
        {
            this.Database = database;
        }
        public bool Entry(Model data)
        {
            data.Id = Convert.ToString(Guid.NewGuid());
            data.EnteryTime = DateTime.Now;
            this.Database.Records.Add(data);

            return true;
        }

        public double Exit(string vehicalNumber)
        {
            var vehical = this.Database.Records.FirstOrDefault(a => a.Status == Status.Parking && a.VehicalNumber == vehicalNumber);
            if (vehical == null)
                return 0;

            vehical.ExitTime = DateTime.Now;
            vehical.Status = Status.Leave;
            TimeSpan time = vehical.ExitTime - vehical.EnteryTime;

            return time.TotalHours+1;
        }

        public List<Model> AllParkingVehical()
        {
            return this.Database.Records.Where(a => a.Status == Status.Parking).ToList();
        }

        public int VehicalType(string vehicalNumber)
        {
            var parkVehical = this.Database.Records.FirstOrDefault(a => a.VehicalNumber == vehicalNumber);
            if (parkVehical == null)
                return 0;

            return parkVehical.Type;
        }
    }
}
