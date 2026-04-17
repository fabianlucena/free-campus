using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;

namespace RCBACEF.Repository
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(DbContext _context) : base(_context)
        {
        }

        public override async Task<Device> ValidateForCreation(Device device)
        {
            device = await base.ValidateForCreation(device);

            if (string.IsNullOrEmpty(device.Token))
            {
                throw new ArgumentException("Device token cannot be null or empty.");
            }

            if (await GetFirstOrDefaultByTokenAsync(device.Token) != null)
            {
                throw new InvalidOperationException("A device with the same token already exists.");
            }

            return device;
        }

        public async Task<Device> CreateAsync(Device device)
        {
            device = await ValidateForCreation(device);

            var table = context.Set<Device>();
            table.Add(device);
            await context.SaveChangesAsync();

            return device;
        }

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
