using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ParkingSlot
{
    class Program
    {
        static void Main()
        {
            int typesOfVehical;
            while (true)
            {
                Console.Write("How many types of vehical is parked: ");
                Int32.TryParse(Console.ReadLine(), out typesOfVehical);
                if (typesOfVehical > 0)
                    break;
                else
                    Console.WriteLine("enter correct value");
            }
            new ParkingRatesList();
            for(int i = 0; i < typesOfVehical; i++)
            {
                RateChart parkingRate = new RateChart();
                while (true)
                {
                    Console.Write((i + 1) + ". Enter type of vehical: ");
                    Int32.TryParse(Console.ReadLine(), out int vehicalType);
                    if (vehicalType > 0 && ParkingRatesList.ParkingRates.FirstOrDefault(a=>a.Type==vehicalType)==null)
                    {
                        parkingRate.Type = vehicalType;
                        break;
                    }
                    Console.WriteLine("Enter correct type");
                }
                Console.Write("Enter vehical rate: ");
                while (true)
                {
                    float.TryParse(Console.ReadLine(), out float rate);
                    if (rate > 0)
                    {
                        parkingRate.Rate = rate;
                        break;
                    }
                    Console.WriteLine("Enter correct rate");
                }
                Console.Write("Enter no of slots: ");
                while (true)
                {
                    Int32.TryParse(Console.ReadLine(), out int slot);
                    if (slot > 0)
                    {
                        parkingRate.NoOfSlots = slot;
                        ParkingRatesList.ParkingRates.Add(parkingRate);
                        break;
                    }
                    Console.WriteLine("Enter correct number of slots");
                }
            }

            Database database = new Database();
            IService service = new Service(database);

            while (true)
            {
                Console.WriteLine(Contraints.VehicalEntry);
                Console.WriteLine(Contraints.VehicalExit);
                Console.WriteLine(Contraints.AllParkingVehicals);
                Console.WriteLine(Contraints.Exit);

                Operations operation = (Operations)Convert.ToInt32(Console.ReadLine());
                switch (operation)
                {
                    case Operations.VehicalEntry:
                        var data = VehicalEntry();
                        if(ParkingRatesList.ParkingRates.FirstOrDefault(a=>a.Type==data.Type).NoOfSlots>0 )
                        service.Entry(data);
                        Console.WriteLine("Slot full no space available");
                        break;

                    case Operations.VehicalExit:
                        string vehicalNumber = VehicalExit();
                        Rate((int)service.Exit(vehicalNumber), service.VehicalType(vehicalNumber));

                        break;

                    case Operations.AllParkingVehical:
                        AllParkingVehical(service.AllParkingVehical());
                        break;

                    case Operations.ExitApplication:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static Model VehicalEntry() 
        {
            Model data = new Model();
            Console.Write("Enter Vehical Number: ");
            data.VehicalNumber = Console.ReadLine();
            while (true) 
            {
                int count = 1;
                foreach(var vehical in ParkingRatesList.ParkingRates)
                {
                    Console.Write(count + ". " + vehical.Type + " Wheeler\n");
                    count++;
                }
                Int32.TryParse(Console.ReadLine(),out int choice);

                if (choice <= count - 1 && choice >= 0) 
                {
                    data.Type = ParkingRatesList.ParkingRates[choice-1].Type;
                    break;
                }
                Console.WriteLine("Enter Correct Input");
            }

            return data;
        }

        static string VehicalExit() 
        {
            Console.WriteLine("Enter the vehical number");
            return Console.ReadLine();
        }

        static void AllParkingVehical(List<Model> vehicals)
        {
            if (vehicals.Any())
            {
                foreach (var vehical in vehicals)
                {
                    Console.WriteLine("Vehical Number: " + vehical.VehicalNumber);
                    Console.WriteLine("Vehical Type  : " + vehical.Type + "vehical");
                    Console.WriteLine("Entry Time    : " + vehical.EnteryTime);
                    Console.WriteLine("\n");
                }
            }
            else
            {
                Console.WriteLine("No vehical is parked now");
            }
        }

        static bool Rate(int duration, int vehicalType)
        {
            var Rate = ParkingRatesList.ParkingRates.FirstOrDefault(a => a.Type == vehicalType);

            if (Rate == null)
            {
                Console.WriteLine("This vehical can't park \n");
                return false;
            }

            Console.WriteLine("Total cost : " + Rate.Rate * duration);
            return true;
        }
    }
}
