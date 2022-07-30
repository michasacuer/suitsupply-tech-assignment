using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suitsupply.Alteration.Api.Consts;

namespace Suitsupply.Alteration.Api.Controllers.Base;

[ApiController]
[Authorize(Roles = Roles.ReadWrite)]
[Route("api/alterations/")]
public abstract class BaseSuitsupplyController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator
                                                  ?? throw new ArgumentNullException(nameof(Mediator));
}