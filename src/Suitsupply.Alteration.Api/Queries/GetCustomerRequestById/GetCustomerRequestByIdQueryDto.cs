using MediatR;

namespace Suitsupply.Alteration.Api.Queries.GetCustomerRequestById;

public class GetCustomerRequestByIdQueryDto : IRequest<GetCustomerRequestByIdResponse>
{
    public Guid Id { get; set; }
}