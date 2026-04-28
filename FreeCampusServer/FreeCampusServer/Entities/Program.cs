using RFBaseEntities.Entities;
using RFRGCBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("Programs", Schema = "fc")]
    public class Program : CommonEntity
    {
        public long TypeId { get; set; }
        public ProgramType? Type { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public long CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
