using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Worktop.Core.Extensions
{
    public static class HttpExtensions
    {
        public static int GetCurrentUserId(this HttpContext httpContext) => int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}