using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;

namespace FreeCampusServer
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
    }
}
