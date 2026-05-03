using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class ProgramEnrollmentStatusRepository(AppDbContext appContext)
        : TranslatableEntityRepository<ProgramEnrollmentStatus>(appContext),
        IProgramEnrollmentStatusRepository
    {
        public override IQueryable<ProgramEnrollmentStatus> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is ProgramEnrollmentStatusQueryOptions programEnrollmentStatusOptions)
            {
                if (programEnrollmentStatusOptions.IncludeOrganization)
                    queryable = queryable.Include(t => t.Organization);

                if (programEnrollmentStatusOptions.OrganizationId.HasValue)
                    queryable = queryable.Where(t => t.OrganizationId == programEnrollmentStatusOptions.OrganizationId.Value);
            }

            return queryable;
        }
    }
}
