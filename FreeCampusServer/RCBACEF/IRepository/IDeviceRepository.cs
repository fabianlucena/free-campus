using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface IDeviceRepository : IBaseRepository<Device>
    {
        Task<Device?> GetFirstOrDefaultByTokenAsync(string token);
    }
}