using RCBACEF.Models;

namespace RCBACEF.IRepository
{
    public interface IDeviceRepository : ICreatableEntityRepository<Device>
    {
        Task<Device?> GetFirstOrDefaultByTokenAsync(string token);
    }
}