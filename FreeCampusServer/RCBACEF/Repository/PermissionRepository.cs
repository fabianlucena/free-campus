using Microsoft.EntityFrameworkCore;
using RCBACEF.IRepository;
using RCBACEF.Models;

namespace RCBACEF.Repository
{
    public class PermissionRepository
        : CreatableEntityRepository<Permission>,
        IPermissionRepository
    {
        public PermissionRepository(DbContext context) : base(context) { }
    }
}
