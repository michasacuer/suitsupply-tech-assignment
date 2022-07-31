using AutoMapper;
using MediatR;
using Suitsupply.Alteration.Api.Dtos;
using Suitsupply.Alteration.Api.Services;
using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;

namespace Suitsupply.Alteration.Api.Queries.GetAllAlterations;

public class GetAllAlterationsQueryHandler : IRequestHandler<GetAllAlterationsQueryDto, GetAllAlterationsResponse>
{
    private readonly ICustomerRequestRepository _customerRequestRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextFacade _httpContextFacade;

    public GetAllAlterationsQueryHandler(
        ICustomerRequestRepository customerRequestRepository,
        IMapper mapper,
        IHttpContextFacade httpContextFacade)
    {
        _customerRequestRepository = customerRequestRepository ?? throw new ArgumentNullException(nameof(customerRequestRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextFacade = httpContextFacade ?? throw new ArgumentNullException(nameof(httpContextFacade));
    }

    public async Task<GetAllAlterationsResponse> Handle(GetAllAlterationsQueryDto request, CancellationToken cancellationToken)
    {
        string appId = _httpContextFacade.GetAppIdFromClaim();
        Ensure.StringNotNullOrEmpty(appId, nameof(appId));
        
        var customerRequests = await _customerRequestRepository.GetAllCustomerRequestsAsync(appId);

        return new GetAllAlterationsResponse
        {
            CustomerRequests = customerRequests
                .Select(x => _mapper.Map<CustomerRequest, CustomerRequestDto>(x))
                .ToList()
        };
    }
}