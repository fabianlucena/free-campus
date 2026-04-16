using Microsoft.AspNetCore.Mvc;
using RCBACEF.DTO;
using RCBACEF.IServices;

namespace RCBACEF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = (await userService.GetListAsync())
                .Select(user => new UserResponse(user));

            return Ok(response);
        }
    }
}
