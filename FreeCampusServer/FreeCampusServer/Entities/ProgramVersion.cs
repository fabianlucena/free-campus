using RFBaseEntities.Entities;
using RFRGOBACEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("ProgramVersions", Schema = "fc")]
    public class ProgramVersion : CommonEntity
    {
        public long ProgramId { get; set; }
        public Program? Program { get; set; }

        public int VersionNumber { get; set; }
        public string? VersionLabel { get; set; }
        public long? PreviousVersionId { get; set; }
        public ProgramVersion? PreviousVersion { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? TotalCredists { get; set; }
    }
}
