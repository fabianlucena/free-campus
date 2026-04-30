using FreeCampusServer.DTO;
using FreeCampusServer.Exceptions;
using FreeCampusServer.IServices;
using Microsoft.AspNetCore.Mvc;
using RFPermissionsEntities.Attributes;
using RFRGOBACEntities.Entities;

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

            var organizationId = HttpContext.Items["OrganizationId"] as long?
                ?? throw new NoOrganizationIdException();

            var courses = await courseService.GetListAvailableByOrganizationIdAsync(organizationId);
            var coursesResponse = courses.Select(course => new CourseResponse(course));

            return Ok(coursesResponse);
        }
    }
}
