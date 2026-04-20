using Microsoft.AspNetCore.Mvc;
using RCBACEF.DTO;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.Services;

namespace RCBACEF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController(
        ILoginService loginService,
        IRoleXUserService roleXUserService,
        IPermissionXRoleService permissionXRoleService
    ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest request)
        {
            var session = await loginService.LoginAsync(request);
            var companies = await roleXUserService.GetCompaniesListByUserIdAsync(session.UserId);
            CompanyMinDTO? currentCompany;
            IEnumerable<string>? roles;
            IEnumerable<string>? permissions;

            if (session.CompanyId != null)
            {
                currentCompany = new CompanyMinDTO(companies.First());

                var rolesId = await roleXUserService.GetAllRolesIdByUserIdAndCompanyIdAsync(session.UserId, session.CompanyId.Value);
                roles = await roleXUserService.GetAllRolesNameByRolesIdAsync(rolesId);

                permissions = await permissionXRoleService.GetAllPermissionsNameForRolesIdAsync(rolesId);
            } else
            {
                currentCompany = null;
                roles = null;
                permissions = null;
            }

            var response = new SessionResponse(session)
            {
                User = new UserMinDTO(session.User!),
                Companies = companies.Select(c => new CompanyMinDTO(c)),
                CurrentCompany = currentCompany,
                Roles = roles,
                Permissions = permissions,
            };

            return Ok(response);
        }
    }
}
