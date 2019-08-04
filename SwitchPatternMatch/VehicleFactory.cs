namespace SwitchPatternMatch
{
    public class VehicleFactoryPayload
    {
        public string TypeName { get; }

        public VehicleFactoryPayload(string typeName)
        {
            TypeName = typeName;
        }
    }
    
    public class VehicleFactory
    {
        public static IVehicle Create(VehicleFactoryPayload options) =>
            options switch
                {
                {TypeName: "car"} => (IVehicle) new Car(),
                {TypeName: "truck"} => (IVehicle) new Truck(),
                {TypeName: "motorcycle"} => (IVehicle) new Motorcycle(),
                _ => null
            };
    }
}