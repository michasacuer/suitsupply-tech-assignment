using MediatR;
using Newtonsoft.Json;
using Suitsupply.Alteration.Api.Dtos;
using Suitsupply.Alteration.Api.Services;
using Suitsupply.Alteration.Common.Exceptions;
using Suitsupply.Alteration.Common.Interfaces;
using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Domain.AlterationAggregate;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;

namespace Suitsupply.Alteration.Api.Commands.SendCustomerRequest;

public class SendCustomerRequestCommandHandler : IRequestHandler<SendCustomerRequestCommandDto, NewCustomerRequestDto>
{
    private readonly ICustomerRequestRepository _customerRequestRepository;
    private readonly IHttpContextFacade _httpContextFacade;
    private readonly IClock _clock;

    public SendCustomerRequestCommandHandler(
        ICustomerRequestRepository customerRequestRepository,
        IHttpContextFacade httpContextFacade,
        IClock clock)
    {
        _customerRequestRepository = customerRequestRepository ?? throw new ArgumentNullException(nameof(customerRequestRepository));
        _httpContextFacade = httpContextFacade ?? throw new ArgumentNullException(nameof(httpContextFacade));
        _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    }

    public async Task<NewCustomerRequestDto> Handle(SendCustomerRequestCommandDto request, CancellationToken cancellationToken)
    {
        string appId = _httpContextFacade.GetAppIdFromClaim();
        Ensure.StringNotNullOrEmpty(appId, nameof(appId));
        
        var shortenInfo = new ShortenInfo(
            request.LeftTrouserLegShortenBy,
            request.RightTrouserLegShortenBy,
            request.LeftSleeveShortenBy,
            request.RightSleeveShortenBy);

        var customerRequest = new CustomerRequest(
            appId,
            request.CustomerName,
            request.CustomerEmail,
            shortenInfo,
            _clock.Now,
            JsonConvert.SerializeObject(shortenInfo));

        var id = await _customerRequestRepository.SendCustomerRequestAsync(customerRequest);

        return new NewCustomerRequestDto
        {
            Id = id
        };
    }
}