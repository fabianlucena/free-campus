using FreeCampusServer.Entities;
using FreeCampusServer.IRepository;
using FreeCampusServer.QueryOptions;
using Microsoft.EntityFrameworkCore;
using RFBaseEF.Repositories;
using RFBaseEntities.QueryOptions;

namespace FreeCampusServer.Repository
{
    public class CourseRepository(AppDbContext appContext)
        : CreatableEntityRepository<Course>(appContext),
        ICourseRepository
    {
        public override IQueryable<Course> CreateDBSet(BaseQueryOptions? options = null)
        {
            var queryable = base.CreateDBSet(options);

            if (options is CourseQueryOptions courseOptions)
            {
                if (courseOptions.IncludeOrganization)
                    queryable = queryable.Include(c => c.Organization);

                if (courseOptions.IncludeType)
                    queryable = queryable.Include(c => c.Type);

                if (courseOptions.OrganizationId is not null)
                    queryable = queryable.Where(c => c.OrganizationId == courseOptions.OrganizationId);

                if (courseOptions.IsStandalone is not null)
                    queryable = queryable.Where(c => c.IsStandalone == courseOptions.IsStandalone);

                if (courseOptions.IsStandaloneOrEnrolledInProgram)
                {
                    var cvSet = appContext.CourseVersions;
                    var peSet = appContext.ProgramEnrollments;
                    var pxcSet = appContext.ProgramVersionXCourseVersion;
                    queryable = queryable.Where(c =>
                        c.IsStandalone ||
                        cvSet.Any(cv =>
                            cv.CourseId == c.Id &&
                            pxcSet.Any(pxc =>
                                pxc.CourseVersionId == cv.Id &&
                                peSet.Any(p =>
                                    p.ProgramVersionId == pxc.ProgramVersionId &&
                                    p.StudentId == courseOptions.StudentId
                                )
                            )
                        )
                    );
                }

                if (courseOptions.StudentId is not null || courseOptions.ExcludeStudentId is not null)
                {
                    var ceSet = appContext.CourseEnrollments.AsQueryable();
                    var cvSet = appContext.CourseVersions;

                    if (courseOptions.StudentId is not null)
                        queryable = queryable.Where(c =>
                            cvSet.Any(cv =>
                                cv.CourseId == c.Id &&
                                ceSet.Any(ce => ce.CourseVersionId == cv.Id &&
                                    ce.StudentId == courseOptions.StudentId
                                )
                            )
                        );

                    if (courseOptions.ExcludeStudentId is not null)
                        queryable = queryable.Where(c =>
                            cvSet.Any(cv =>
                                cv.CourseId == c.Id &&
                                !ceSet.Any(ce => ce.CourseVersionId == cv.Id &&
                                    ce.StudentId == courseOptions.ExcludeStudentId
                                )
                            )
                        );
                }
            }

            return queryable;
        }

        public async Task<IEnumerable<Course>> GetStandaloneListByOrganizationIdAsync(long organizationId, CourseQueryOptions? options = null)
        {
            var set = GetDBSet(options);
            var session = await set
                .Where(c => c.OrganizationId == organizationId
                    && c.IsStandalone
                )
                .ToListAsync();

            return session;
        }
    }
}
