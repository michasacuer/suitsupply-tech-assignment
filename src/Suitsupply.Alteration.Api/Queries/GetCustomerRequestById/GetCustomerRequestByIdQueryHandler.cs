using AutoMapper;
using MediatR;
using Suitsupply.Alteration.Api.Dtos;
using Suitsupply.Alteration.Api.Extensions;
using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;

namespace Suitsupply.Alteration.Api.Queries.GetCustomerRequestById;

public class GetCustomerRequestByIdQueryHandler : IRequestHandler<GetCustomerRequestByIdQueryDto, GetCustomerRequestByIdResponse>
{
    private readonly ICustomerRequestRepository _customerRequestRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetCustomerRequestByIdQueryHandler(
        IMapper mapper,
        ICustomerRequestRepository customerRequestRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _customerRequestRepository = customerRequestRepository ?? throw new ArgumentNullException(nameof(customerRequestRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GetCustomerRequestByIdResponse> Handle(GetCustomerRequestByIdQueryDto request, CancellationToken cancellationToken)
    {
        string appId = _httpContextAccessor.HttpContext?.GetAppIdFromClaim();
        Ensure.StringNotNullOrEmpty(appId, nameof(appId));
        
        var customerRequest = await _customerRequestRepository.GetCustomerRequestByIdAsync(request.Id, appId);

        return new GetCustomerRequestByIdResponse
        {
            CustomerRequest = _mapper.Map<CustomerRequest, CustomerRequestDto>(customerRequest)
        };
    }
}