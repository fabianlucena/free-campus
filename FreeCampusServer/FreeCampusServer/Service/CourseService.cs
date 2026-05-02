using FreeCampusServer.Entities;
using FreeCampusServer.Exceptions;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseService(
        ICourseRepository courseRepository
    )
        : CommonEntityService<Course>(courseRepository),
        ICourseService
    {
        public async Task<IEnumerable<Course>> GetAvailableListAsync(CourseQueryOptions options)
        {
            if (options.OrganizationId is null)
                throw new NoOrganizationIdException();

            if (options.StudentId is null)
                throw new NoStudentIdException();

            var availableList = await GetListAsync(new CourseQueryOptions(options)
            {
                IsStandaloneOrEnrolledInProgram = true,
                StudentId = null,
                ExcludeStudentId = options.StudentId,
            });

            return availableList;
        }

        public async Task<IEnumerable<Course>> GetMineListAsync(CourseQueryOptions options)
        {
            if (options.OrganizationId is null)
                throw new NoOrganizationIdException();

            if (options.StudentId is null)
                throw new NoStudentIdException();

            var availableList = await GetListAsync(new CourseQueryOptions(options)
            {
                IsStandaloneOrEnrolledInProgram = true,
                StudentId = null,
                ExcludeStudentId = options.StudentId,
            });

            return availableList;
        }
    }
}
