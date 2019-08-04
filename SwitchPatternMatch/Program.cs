using System;

namespace SwitchPatternMatch
{
    class Program
    {
        static void Main(string[] args)
        {
            var vehicle = VehicleFactory.Create(new VehicleFactoryPayload(args[0]));
            vehicle?.Run();
        }
    }
}