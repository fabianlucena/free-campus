using Microsoft.AspNetCore.Mvc;
using RCBAC.DTO;
using RCBAC.IServices;

namespace RCBAC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = (await userService.GetList())
                .Select(user => new UserResponse(user));

            return Ok(response);
        }
    }
}
