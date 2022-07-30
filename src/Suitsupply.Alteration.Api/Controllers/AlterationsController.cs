using Microsoft.AspNetCore.Mvc;
using Suitsupply.Alteration.Api.Controllers.Base;
using Suitsupply.Alteration.Api.Queries.GetAllAlterations;

namespace Suitsupply.Alteration.Api.Controllers;

public class AlterationsController : BaseSuitsupplyController
{
    [HttpGet]
    [Route("healthcheck")]
    public IActionResult Healthcheck() => new OkResult();
    
    [HttpGet]
    public async Task<GetAllAlterationsResponse> GetAllAlternationsAsync()
        => await Mediator.Send(new GetAllAlterationsQueryDto());
}