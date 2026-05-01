using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeCampusServer
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ProgramType> ProgramTypes { get; set; }
        //public DbSet<Program> Programs { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ModuleType> ModuleTypes { get; set; }
        public DbSet<Module> Modules { get; set; }

        public DbSet<ProgramEnrollment> ProgramEnrollments { get; set; }
        public DbSet<CourseXProgram> CoursesXPrograms { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }
        }
    }
}
