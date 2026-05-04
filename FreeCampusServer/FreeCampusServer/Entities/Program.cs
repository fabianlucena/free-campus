using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("Programs", Schema = "fc")]
    public sealed class Program : CommonEntity
    {
        public long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public long TypeId { get; set; }
        public ProgramType? Type { get; set; }

        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Program() { }

        public Program(Program? entity = null)
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
        }

        public override Program Clone()
            => new(this);
    }
}
