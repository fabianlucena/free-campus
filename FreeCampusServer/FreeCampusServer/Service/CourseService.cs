using FreeCampusServer.Entities;
using FreeCampusServer.Exceptions;
using FreeCampusServer.IRepository;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using RFBaseEntities.QueryOptions;
using RFBaseServices.Services;

namespace FreeCampusServer.Service
{
    public class CourseService(
        ICourseRepository courseRepository,
        IServiceProvider serviceProvider
    )
        : CommonEntityService<Course>(courseRepository),
        ICourseService
    {
        public ICourseTypeService? courseTypeService;
        public ICourseTypeService CourseTypeService
        {
            get
            {
                courseTypeService ??= serviceProvider.GetRequiredService<ICourseTypeService>();
                return courseTypeService!;
            }
        }

        public override async Task<IEnumerable<Course>> GetListAsync(BaseQueryOptions? options = null)
        {
            var courses = await base.GetListAsync(options);

            if (options is CourseQueryOptions courseOptions)
            {
                if (courseOptions.Translate)
                {
                    courses = await Translate(courses);
                }
            }

            return courses;
        }

        public async Task<IEnumerable<Course>> Translate(IEnumerable<Course> courses)
        {
            var results = await Task.WhenAll(courses.Select(course => Translate(course)));

            return results;
        }

        public async Task<Course> Translate(Course course)
        {
            if (course.Type is not null)
                course.Type = await CourseTypeService.Translate(course.Type);

            return course;
        }

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
    }
}
