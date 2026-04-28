using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("Modules", Schema = "fc")]
    public class Module : CommonEntity
    {
        public long TypeId { get; set; }
        public ModuleType? Type { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public long CourseId { get; set; }
        public Course? Course { get; set; }

    }
}
