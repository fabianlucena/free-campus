using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class ProgramEnrollmentRepository(AppDbContext appContext)
        : CreatableEntityRepository<ProgramEnrollment>(appContext),
        IProgramEnrollmentRepository
    {
        public override IQueryable<ProgramEnrollment> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is ProgramEnrollmentQueryOptions programEnrollmentOptions)
            {
                if (programEnrollmentOptions.IncludeProgram)
                    queryable = queryable.Include(pe => pe.ProgramVersion)
                        .ThenInclude(pv => pv!.Program);
                else if (programEnrollmentOptions.IncludeProgramVersion)
                    queryable = queryable.Include(pe => pe.ProgramVersion);

                if (programEnrollmentOptions.IncludeStudent)
                    queryable = queryable.Include(pe => pe.Student);
            }

            return queryable;
        }
    }
}
