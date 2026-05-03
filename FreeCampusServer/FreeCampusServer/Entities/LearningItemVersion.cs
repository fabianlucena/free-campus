using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("LearningItemVersions", Schema = "fc")]
    public class LearningItemVersion : CommonEntity
    {
        public long LearningItemId { get; set; }
        public LearningItem? LearningItem { get; set; }

        public int VersionNumber { get; set; }
        public string? VersionLabel { get; set; }
        public long? PreviousVersionId { get; set; }
        public LearningItemVersion? PreviousVersion { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public bool IsPublisehd { get; set; }
    }
}
