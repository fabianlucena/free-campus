using Microsoft.AspNetCore.Mvc;
using RCBACEF.DTO;
using RCBACEF.IServices;
using RCBACEF.QueryOptions;

namespace RCBACEF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = (await userService.GetListAsync(new UserOptions
            {
                IncludeCreatedBy = true,
                IncludeUpdatedBy = true,
                IncludeDeletedBy = true,
            }))
                .Select(user => new UserResponse(user));

            return Ok(response);
        }
    }
}
