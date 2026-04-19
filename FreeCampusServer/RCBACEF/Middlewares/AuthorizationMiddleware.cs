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
                    if (!cache.TryGetValue(token, out var cachedSession)
                        || cachedSession is null)
                    {
                        var session = await sessionService.GetFirstOrDefaultByTokenAsync(token, new SessionQueryOptions { IncludeUser = true, IncludeDevice = true });
                        if (session != null)
                        {
                            cachedSession = new SessionCache
                            {
                                Token = token,
                                SessionId = session.Id,
                                UserId = session.UserId,
                                Session = session,
                                User = session.User!,
                                Device = session.Device!
                            };

                            cache[token] = cachedSession;
                        }
                    }

                    if (cachedSession is not null)
                    {
                        context.Items["SessionId"] = cachedSession.SessionId;
                        context.Items["UserId"] = cachedSession.UserId;
                        context.Items["Session"] = cachedSession.Session;
                        context.Items["User"] = cachedSession.User;
                        context.Items["Device"] = cachedSession.Device;
                        await sessionService.UpdateLastUsageAsync(cachedSession.SessionId);
                    }
                }
            }

            await next(context);
        }
    }
}
