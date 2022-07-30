using MediatR;

namespace Suitsupply.Alteration.Api.Commands.SendCustomerRequest;

public class SendCustomerRequestCommandDto : IRequest
{
    public string CustomerName { get; init; }

    public string CustomerEmail { get; init; }

    public int LeftTrouserLegShortenBy { get; init; }

    public int RightTrouserLegShortenBy { get; init; }

    public int LeftSleeveShortenBy { get; init; }

    public int RightSleeveShortenBy { get; init; }
}