using FreeCampusServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeCampusServer
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ProgramType> ProgramTypes { get; set; }
        public DbSet<Entities.Program> Programs { get; set; }
        public DbSet<ProgramVersion> ProgramVersions { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseVersion> CourseVersions { get; set; }
        public DbSet<LearningItemType> LearningItemTypes { get; set; }
        public DbSet<LearningItem> LearningItems { get; set; }
        public DbSet<LearningItemVersion> LearningItemVersions { get; set; }

        public DbSet<ProgramEnrollment> ProgramEnrollments { get; set; }
        public DbSet<ProgramVersionXCourseVersion> ProgramVersionXCourseVersion { get; set; }
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
