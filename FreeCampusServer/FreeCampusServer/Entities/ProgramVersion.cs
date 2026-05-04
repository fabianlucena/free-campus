using RFBaseEntities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCampusServer.Entities
{
    [Table("ProgramVersions", Schema = "fc")]
    public sealed class ProgramVersion : CommonEntity
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

        public ProgramVersion() { }

        public ProgramVersion(ProgramVersion? entity = null)
            : base(entity)
        {
            if (entity == null)
                return;

            ProgramId = entity.ProgramId;
            Program = entity.Program;

            VersionNumber = entity.VersionNumber;
            VersionLabel = entity.VersionLabel;
            PreviousVersionId = entity.PreviousVersionId;
            PreviousVersion = entity.PreviousVersion;

            Title = entity.Title;
            Description = entity.Description;
            TotalCredists = entity.TotalCredists;
        }

        public override ProgramVersion Clone()
            => new(this);
    }
}
