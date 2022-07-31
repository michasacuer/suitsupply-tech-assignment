using AutoMapper;
using MediatR;
using Suitsupply.Alteration.Api.Dtos;
using Suitsupply.Alteration.Api.Extensions;
using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;
using Suitsupply.Alteration.Infrastructure.EmailSender;

namespace Suitsupply.Alteration.Api.Queries.GetAllAlterations;

public class GetAllAlterationsQueryHandler : IRequestHandler<GetAllAlterationsQueryDto, GetAllAlterationsResponse>
{
    private readonly ICustomerRequestRepository _customerRequestRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllAlterationsQueryHandler(
        ICustomerRequestRepository customerRequestRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _customerRequestRepository = customerRequestRepository ?? throw new ArgumentNullException(nameof(customerRequestRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<GetAllAlterationsResponse> Handle(GetAllAlterationsQueryDto request, CancellationToken cancellationToken)
    {
        string appId = _httpContextAccessor.HttpContext?.GetAppIdFromClaim();
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