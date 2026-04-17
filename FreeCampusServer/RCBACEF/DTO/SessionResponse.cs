using RCBACEF.Models;

namespace RCBACEF.DTO
{
    public class SessionResponse(Session session)
    {
        public string Token { get; } = session.Token;
        public DateTime ExpireAt { get; } = session.ExpireAt;
        public string AutoLoginToken { get; } = session.AutoLoginToken;
        public string DeviceToken { get; } = session.Device?.Token ?? string.Empty;
    }
}
