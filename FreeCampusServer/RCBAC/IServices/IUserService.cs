using RCBAC.Models;

namespace RCBAC.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetList();
    }
}
