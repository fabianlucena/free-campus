using RCBACEF.Models;

namespace RCBACEF.IServices
{
    public interface IDeviceService : IBaseService<Device>
    {
        Task<Device> CreateAsync();
        Task<Device> GetFirstOrCreateByTokenAsync(string deviceToken);
    }
}
