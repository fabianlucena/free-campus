using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;

namespace RCBACEF.Repository
{
    public class SoftDeletableRepository<T> : AuditableRepository<T> where T : SoftDeletable
    {
        public SoftDeletableRepository(DbContext context) : base(context)
        {
        }
    }
}
