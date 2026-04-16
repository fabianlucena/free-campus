using RCBAC.Models;

namespace RCBAC.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetListAsync();
    }
}
