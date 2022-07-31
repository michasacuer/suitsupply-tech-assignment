using AutoMapper;
using MediatR;
using Suitsupply.Alteration.Api.Dtos;
using Suitsupply.Alteration.Api.Extensions;
using Suitsupply.Alteration.Api.Services;
using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;

namespace Suitsupply.Alteration.Api.Queries.GetCustomerRequestById;

public class GetCustomerRequestByIdQueryHandler : IRequestHandler<GetCustomerRequestByIdQueryDto, GetCustomerRequestByIdResponse>
{
    private readonly ICustomerRequestRepository _customerRequestRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextFacade _httpContextFacade;

    public GetCustomerRequestByIdQueryHandler(
        IMapper mapper,
        ICustomerRequestRepository customerRequestRepository,
        IHttpContextFacade httpContextFacade)
    {
        _customerRequestRepository = customerRequestRepository ?? throw new ArgumentNullException(nameof(customerRequestRepository));
        _httpContextFacade = httpContextFacade ?? throw new ArgumentNullException(nameof(httpContextFacade));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GetCustomerRequestByIdResponse> Handle(GetCustomerRequestByIdQueryDto request, CancellationToken cancellationToken)
    {
        string appId = _httpContextFacade.GetAppIdFromClaim();
        Ensure.StringNotNullOrEmpty(appId, nameof(appId));
        
        var customerRequest = await _customerRequestRepository.GetCustomerRequestByIdAsync(request.Id, appId);

        return new GetCustomerRequestByIdResponse
        {
            CustomerRequest = _mapper.Map<CustomerRequest, CustomerRequestDto>(customerRequest)
        };
    }
}