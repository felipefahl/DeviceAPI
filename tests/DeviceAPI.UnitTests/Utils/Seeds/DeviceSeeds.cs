using Core.Entities;

namespace DeviceAPI.UnitTests.Utils.Seeds
{
    public static class DeviceSeeds
    {
        public static IList<Device> Devices => new List<Device>
                {
                    new Device ("Device 1", "Brand 1"),
                    new Device ("Device 2", "Brand 2"),
                    new Device ("Device 3", "Brand 1"),
                    new Device ("Device 4", "Brand 3")
                };
    }
}
