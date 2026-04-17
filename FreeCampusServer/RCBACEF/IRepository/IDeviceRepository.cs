using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface IDeviceRepository
    {
        Task<Device> CreateAsync(Device device);
        Task<Device?> GetFirstOrDefaultByTokenAsync(string token);
    }
}