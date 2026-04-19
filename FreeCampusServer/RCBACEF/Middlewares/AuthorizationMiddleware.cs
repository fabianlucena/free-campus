using Microsoft.AspNetCore.Http;
using RCBACEF.IServices;
using RCBACEF.Models;
using RCBACEF.QueryOptions;

namespace RCBACEF.Middlewares
{
    public class SessionCache
    {
        public required string Token { get; set; }
        public required Int64 SessionId { get; set; }
        public required Int64 UserId { get; set; }
        public required Session Session { get; set; }
        public required User User { get; set; }
        public required Device Device { get; set; }
    }

    public class AuthorizationMiddleware(RequestDelegate next)
    {
        static Dictionary<string, SessionCache> cache = [];

        public async Task InvokeAsync(HttpContext context, ISessionService sessionService)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var authorizationList) && authorizationList.Count > 0)
            {
                foreach (var authorization in authorizationList)
                {
                    if (String.IsNullOrEmpty(authorization) || !authorization[..7].Equals("bearer ", StringComparison.CurrentCultureIgnoreCase))
                        continue;

                    var token = authorization[7..].Trim();
                    if (!cache.TryGetValue(token, out var sessionCache)
                        || sessionCache is null)
                    {
                        var session = await sessionService.GetFirstOrDefaultByTokenAsync(token, new SessionQueryOptions { IncludeUser = true, IncludeDevice = true });
                        if (session != null)
                        {
                            sessionCache = new SessionCache
                            {
                                Token = token,
                                SessionId = session.Id,
                                UserId = session.UserId,
                                Session = session,
                                User = session.User!,
                                Device = session.Device!
                            };
                        }
                    }

                    if (sessionCache is not null)
                    {
                        context.Items["SessionId"] = sessionCache.SessionId;
                        context.Items["UserId"] = sessionCache.UserId;
                        context.Items["Session"] = sessionCache.Session;
                        context.Items["User"] = sessionCache.User;
                        context.Items["Device"] = sessionCache.Device;

                        _ = sessionService.UpdateLastUsageAsync(sessionCache.SessionId);
                    }
                }
            }

            await next(context);
        }
    }
}
