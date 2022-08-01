using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suitsupply.Alteration.Common.Interfaces;
using Suitsupply.Alteration.Infrastructure.Common;
using Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.AlterationFinished;
using Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.OrderPaid;

namespace Suitsupply.Alteration.Api.Controllers;

// only for debugging!
public class DebugController : ControllerBase
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IConfiguration _configuration;
    private readonly DebugClock _clock;

    public DebugController(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration, IClock clock)
    {
        _sendEndpointProvider = sendEndpointProvider ?? throw new ArgumentNullException(nameof(sendEndpointProvider));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _clock = clock as DebugClock ?? throw new ArgumentNullException(nameof(clock));
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
    
    [Authorize]
    [HttpGet]
    [Route("api/debug/current-time")]
    public IActionResult CurrentTime() => new OkObjectResult(_clock.Now);

    [Authorize]
    [HttpPost]
    [Route("api/debug/add-to-now")]
    public IActionResult AddToUtcNow([FromBody] TimeValues values)
    {
        var newDate = DateTime.UtcNow.AddHours(values.HoursToAdd);
        newDate = newDate.AddDays(values.DaysToAdd);
        newDate = newDate.AddMonths(values.MonthsToAdd);
        _clock.SetNow(newDate);

        return new OkResult();
    }

    [Authorize]
    [HttpPost]
    [Route("api/debug/reset-time-to-utcnow")]
    public IActionResult ResetTime()
    {
        _clock.Reset();

        return new OkResult();
    }
}

public class TimeValues
{
    public int HoursToAdd { get; set; } = 0;
    public int DaysToAdd { get; set; } = 0;
    public int MonthsToAdd { get; set; } = 0;
}
