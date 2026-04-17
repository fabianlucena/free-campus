using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using System.Security.Cryptography;

namespace RCBACEF.Services
{
    public class DeviceService(IDeviceRepository deviceRepository) : BaseService<Device>(deviceRepository), IDeviceService
    {   
        public int TokenSize { get; set; } = 32;

        public override async Task<Device> ValidateForCreationAsync(Device device)
        {
            device = await base.ValidateForCreationAsync(device);

            if (string.IsNullOrEmpty(device.Token))
            {
                do
                {
                    byte[] bytes = RandomNumberGenerator.GetBytes(TokenSize);
                    var token = Convert.ToBase64String(bytes);
                    device.Token = token;
                } while (await GetFirstOrDefaultByTokenAsync(device.Token) != null);
            } else if (await GetFirstOrDefaultByTokenAsync(device.Token) != null)
            {
                throw new InvalidOperationException("A device with the same token already exists.");
            }

            return device;
        }

        public async Task<Device> CreateAsync()
        {
            return await CreateAsync(new Device());
        }

        public async Task<Device?> GetFirstOrDefaultByTokenAsync(string token)
        {
            return await deviceRepository.GetFirstOrDefaultByTokenAsync(token);
        }

        public async Task<Device> GetFirstOrCreateByTokenAsync(string token)
        {
            return await deviceRepository.GetFirstOrDefaultByTokenAsync(token)
                ?? await CreateAsync();
        }
    }
}
