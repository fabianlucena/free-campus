using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("LearningItemVersions", Schema = "fc")]
    public sealed class LearningItemVersion : CommonEntity
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

        public LearningItemVersion() { }

        public LearningItemVersion(LearningItemVersion? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            LearningItemId = entity.LearningItemId;
            LearningItem = entity.LearningItem;

            VersionNumber = entity.VersionNumber;
            VersionLabel = entity.VersionLabel;
            PreviousVersionId = entity.PreviousVersionId;
            PreviousVersion = entity.PreviousVersion;

            Title = entity.Title;
            Description = entity.Description;

            IsPublisehd = entity.IsPublisehd;
        }

        public override LearningItemVersion Clone()
            => new(this);
    }
}
