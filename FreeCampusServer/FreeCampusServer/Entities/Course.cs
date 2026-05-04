using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("Courses", Schema = "fc")]
    public sealed class Course : CommonEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public long TypeId { get; set; }
        public CourseType? Type { get; set; }

        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsStandalone { get; set; } = false;

        public Course() { }

        public Course(Course? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            OrganizationId = entity.OrganizationId;
            Organization = entity.Organization;

            TypeId = entity.TypeId;
            Type = entity.Type;

            Code = entity.Code;
            Name = entity.Name;
            IsStandalone = entity.IsStandalone;
        }

        public override Course Clone()
            => new(this);
    }
}
