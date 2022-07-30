namespace Suitsupply.Alteration.Api.Extensions;

public static class HttpContextAccessorExtensions
{
    public static string GetAppIdFromClaim(this HttpContext context)
        => context?.User.Claims.FirstOrDefault(x => x.Type == "appid")?.Value;
}