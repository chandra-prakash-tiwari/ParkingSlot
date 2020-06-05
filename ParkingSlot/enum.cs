using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSlot
{
    public enum VehicalType
    {
        Two=1,
        Three,
        Four
    }

    public enum Operations
    { 
        VehicalEntry=1,
        VehicalExit,
        AllParkingVehical,
        ExitApplication
    }

    public enum Status
    {
        Parking,
        Leave
    }

    public enum ParkingRate
    {
        TwoVehical=5,
        ThreeVehical=10,
        FourVehical=20
    }
}
