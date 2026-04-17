using RCBACEF.IRepository;
using RCBACEF.IServices;
using RCBACEF.Models;
using System.Security.Cryptography;

namespace RCBACEF.Services
{
    public class DeviceService(IDeviceRepository deviceRepository) : IDeviceService
    {
        public int TokenSize { get; set; } = 32;

        public async Task<Device> CreateAsync()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(TokenSize);
            var token = Convert.ToBase64String(bytes);

            var device = new Device
            {
                Token = token,
            };

            return await deviceRepository.CreateAsync(device);
        }

        public async Task<Device> GetFirstOrCreateByTokenAsync(string token)
        {
            return await deviceRepository.GetFirstOrDefaultByTokenAsync(token)
                ?? await CreateAsync();
        }
    }
}
