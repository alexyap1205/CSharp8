using System;

namespace SwitchPatternMatch
{
    public class Car : IVehicle
    {
        public void Run()
        {
            Console.WriteLine("Car Running...");
        }
    }
    
    public class Truck : IVehicle
    {
        public void Run()
        {
            Console.WriteLine("Truck Running...");
        }
    }
    
    public class Motorcycle : IVehicle
    {
        public void Run()
        {
            Console.WriteLine("Motorcycle Running...");
        }
    }
}