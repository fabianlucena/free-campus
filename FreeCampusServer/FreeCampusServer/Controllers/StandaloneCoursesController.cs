using FreeCampusServer.DTO;
using FreeCampusServer.IServices;
using Microsoft.AspNetCore.Mvc;
using RFPermissionsEntities.Attributes;

namespace FreeCampusServer.Controllers
{
    [ApiController]
    [Route("v1/standalone-courses")]
    public class StandaloneCoursesController(
        ILogger<StandaloneCoursesController> logger,
        ICourseService courseService
    ) : ControllerBase
    {
        [HttpGet]
        [Permission("standaloneCourses.view")]
        public async Task<IActionResult> GetAsync()
        {
            logger.LogInformation("Standalone courses");

            var courses = await courseService.GetListByStandaloneAsync();
            var coursesResponse = courses.Select(course => new CourseResponse(course));

            return Ok(coursesResponse);
        }
    }
}
