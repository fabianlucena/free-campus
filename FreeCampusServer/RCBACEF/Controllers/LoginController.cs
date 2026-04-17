using Microsoft.AspNetCore.Mvc;
using RCBACEF.DTO;
using RCBACEF.IServices;

namespace RCBACEF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController(ILoginService loginService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest request)
        {
            var session = await loginService.LoginAsync(request);

            return Ok(new SessionResponse(session));
        }
    }
}
