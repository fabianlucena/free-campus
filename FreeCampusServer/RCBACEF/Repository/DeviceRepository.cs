using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;
namespace RCBACEF.Repository
{
    public class DeviceRepository : CreatableEntityRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(DbContext context) : base(context) { }

        public async Task<Device?> GetFirstOrDefaultByTokenAsync(string token)
        {
            var table = context.Set<Device>();
            var device = await table
                .Where(d => d.Token == token)
                .FirstOrDefaultAsync();

            return device;
        }
    }
}
