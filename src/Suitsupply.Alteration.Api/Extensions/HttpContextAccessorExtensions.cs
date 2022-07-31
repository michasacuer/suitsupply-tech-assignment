namespace Suitsupply.Alteration.Api.Extensions;

internal static class HttpContextAccessorExtensions
{
    internal static string GetAppIdFromClaim(this HttpContext context)
        => context?.User.Claims.FirstOrDefault(x => x.Type == "appid")?.Value;
}