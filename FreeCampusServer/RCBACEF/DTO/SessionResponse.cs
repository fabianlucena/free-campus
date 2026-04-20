using RCBACEF.Models;

namespace RCBACEF.DTO
{
    public class SessionResponse(Session session)
    {
        public string Token { get; } = session.Token;
        public DateTime ExpireAt { get; } = session.ExpireAt;
        public string AutoLoginToken { get; } = session.AutoLoginToken;
        public string DeviceToken { get; } = session.Device?.Token ?? string.Empty;
        public UserMinDTO? User { get; set; }
        public IEnumerable<CompanyMinDTO>? Companies { get; set; }
        public CompanyMinDTO? CurrentCompany { get; set; }
        public IEnumerable<string>? Roles { get; set; }
        public IEnumerable<string>? Permissions { get; set; }
    }
}
