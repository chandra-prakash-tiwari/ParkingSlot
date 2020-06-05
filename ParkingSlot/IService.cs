using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSlot
{
    public interface IService
    {
        bool Entry(Model data);

        double Exit(string vehicalNumber);

        List<Model> AllParkingVehical();

        int VehicalType(string vehicalNumber);
    }
}
