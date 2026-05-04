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

            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName?.StartsWith("RF") ?? false)
                .Where(a => a.GetReferencedAssemblies()
                    .Any(r => r.Name == "Microsoft.EntityFrameworkCore")
                )
                .ToArray();

            foreach (var assembly in assemblies)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(ToSnakeCase(entity.GetTableName()!));

                foreach (var property in entity.GetProperties())
                    property.SetColumnName(ToSnakeCase(property.GetColumnName()!));

                foreach (var key in entity.GetKeys())
                    key.SetName(ToSnakeCase(key.GetName()!));

                foreach (var fk in entity.GetForeignKeys())
                    fk.SetConstraintName(ToSnakeCase(fk.GetConstraintName()!));

                foreach (var index in entity.GetIndexes())
                    index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()!));
            }
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var chars = new List<char>(input.Length + 10);

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (char.IsUpper(c))
                {
                    if (i > 0)
                        chars.Add('_');

                    chars.Add(char.ToLowerInvariant(c));
                }
                else
                {
                    chars.Add(c);
                }
            }

            return new string(chars.ToArray());
        }
    }
}
