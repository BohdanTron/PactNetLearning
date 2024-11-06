using System.Text.RegularExpressions;

namespace Provider.Middlewares
{
    public class AuthorizationMiddleware
    {
        private const string AuthorizationHeaderKey = "Authorization";
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next) => 
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(AuthorizationHeaderKey))
            {
                var tokenTime = DateTime.Parse(AuthorizationHeader(context.Request));

                if (IsOlderThanOneHour(tokenTime))
                {
                    UnauthorizedResponse(context);
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                UnauthorizedResponse(context);
            }
        }

        private string AuthorizationHeader(HttpRequest request)
        {
            request.Headers.TryGetValue(AuthorizationHeaderKey, out var authorizationHeader);
            var match = Regex.Match(authorizationHeader, "Bearer (.*)");
            return match.Groups[1].Value;
        }

        private static bool IsOlderThanOneHour(DateTime tokenTime) => 
            tokenTime < DateTime.Now.AddHours(-1);

        private static void UnauthorizedResponse(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}
