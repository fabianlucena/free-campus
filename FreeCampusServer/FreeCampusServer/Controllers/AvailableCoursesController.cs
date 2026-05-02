using FreeCampusServer.DTO;
using FreeCampusServer.Exceptions;
using FreeCampusServer.IServices;
using FreeCampusServer.QueryOptions;
using Microsoft.AspNetCore.Mvc;
using RFPermissionsEntities.Attributes;

namespace FreeCampusServer.Controllers
{
    [ApiController]
    [Route("v1/available-courses")]
    public class AvailableCoursesController(
        ILogger<AvailableCoursesController> logger,
        ICourseService courseService
    ) : ControllerBase
    {
        [HttpGet]
        [Permission("availableCourses.view")]
        public async Task<IActionResult> GetAsync()
        {
            logger.LogInformation("GET v1/available-courses");

            var organizationId = HttpContext.Items["CurrentOrganizationId"] as long?
                ?? throw new NoOrganizationIdException();

            var userId = HttpContext.Items["UserId"] as long?
                ?? throw new NoUserIdException();

            var courses = await courseService.GetAvailableListAsync(new CourseQueryOptions {
                OrganizationId = organizationId,
                StudentId = userId,
                Translate = true,
            });
            var coursesResponse = courses.Select(course => new CourseResponse(course));

            return Ok(coursesResponse);
        }
    }
}
