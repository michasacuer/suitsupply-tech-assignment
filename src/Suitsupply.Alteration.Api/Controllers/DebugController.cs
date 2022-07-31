using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.AlterationFinished;
using Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.OrderPaid;

namespace Suitsupply.Alteration.Api.Controllers;

// only for debugging!
public class DebugController : ControllerBase
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IConfiguration _configuration;

    public DebugController(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
    {
        _sendEndpointProvider = sendEndpointProvider ?? throw new ArgumentNullException(nameof(sendEndpointProvider));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    [Authorize]
    [HttpPost]
    [Route("api/debug/send-to-finished-alteration-queue")]
    public async Task<IActionResult> SendToQueueAsync([FromBody] AlterationFinishedMessage msg)
    {
        // only for debugging! I know hard coding is ugly but it exist only for playing with that
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(
            new Uri($"queue:{_configuration["ServiceBus:AlterationFinishedInputQueue"]}"));
        
        await endpoint.Send(msg);

        return Ok();
    }
    
    [Authorize]
    [HttpPost]
    [Route("api/debug/send-to-order-paid-topic")]
    public async Task<IActionResult> SendToTopic([FromBody] OrderPaidMessage msg)
    {
        // only for debugging! I know hard coding is ugly but it exist only for playing with that
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(
            new Uri("topic:suitsupply.alteration.infrastructure.masstransit.consumers.orderpaid/orderpaidmessage"));
        
        await endpoint.Send(msg);

        return Ok();
    }
}