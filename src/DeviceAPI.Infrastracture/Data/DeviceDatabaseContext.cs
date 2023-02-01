using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeviceAPI.Infrastracture.Data
{
    public class DeviceDatabaseContext : DbContext
    {
        public DeviceDatabaseContext(DbContextOptions<DeviceDatabaseContext> options) : base(options)
        {

        }

        public DbSet<Device> Devices { get; set; }
    }
}
