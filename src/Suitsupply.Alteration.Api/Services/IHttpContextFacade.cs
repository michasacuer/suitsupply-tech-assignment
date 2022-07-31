namespace Suitsupply.Alteration.Api.Services;

public interface IHttpContextFacade
{
    string GetAppIdFromClaim();
}