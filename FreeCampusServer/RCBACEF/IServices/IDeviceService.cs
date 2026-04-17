using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IDeviceService
    {
        Task<Device> CreateAsync();
        Task<Device> GetFirstOrCreateByTokenAsync(string deviceToken);
    }
}
