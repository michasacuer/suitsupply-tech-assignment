using Microsoft.AspNetCore.Mvc;
using Suitsupply.Alteration.Api.Commands.SendCustomerRequest;
using Suitsupply.Alteration.Api.Controllers.Base;
using Suitsupply.Alteration.Api.Queries.GetAllAlterations;
using Suitsupply.Alteration.Api.Queries.GetCustomerRequestById;

namespace Suitsupply.Alteration.Api.Controllers;

public class AlterationsController : BaseSuitsupplyController
{
    [HttpGet]
    [Route("healthcheck")]
    public IActionResult Healthcheck() => new OkResult();
    
    [HttpGet]
    public async Task<GetAllAlterationsResponse> GetAllAlternationsAsync()
        => await Mediator.Send(new GetAllAlterationsQueryDto());
    
    [HttpGet]
    [Route("{id}")]
    public async Task<GetCustomerRequestByIdResponse> GetCustomerRequestByIdAsync(string id)
        => await Mediator.Send(new GetCustomerRequestByIdQueryDto
        {
            Id = new Guid(id)
        });

    [HttpPost]
    public async Task SendCustomerRequestAsync(SendCustomerRequestCommandDto request)
        => await Mediator.Send(request);
}