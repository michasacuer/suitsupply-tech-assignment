using MediatR;

namespace Suitsupply.Alteration.Api.Queries.GetCustomerRequestById;

public class GetCustomerRequestByIdQueryDto : IRequest<GetCustomerRequestByIdResponse>
{
    public string Id { get; set; }
}