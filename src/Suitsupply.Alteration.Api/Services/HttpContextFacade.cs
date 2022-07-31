using Suitsupply.Alteration.Api.Extensions;

namespace Suitsupply.Alteration.Api.Services;

public class HttpContextFacade : IHttpContextFacade
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextFacade(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string GetAppIdFromClaim() => _httpContextAccessor?.HttpContext.GetAppIdFromClaim();
}