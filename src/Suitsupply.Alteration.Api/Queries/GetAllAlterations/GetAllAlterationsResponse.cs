using Suitsupply.Alteration.Api.Dtos;

namespace Suitsupply.Alteration.Api.Queries.GetAllAlterations;

public class GetAllAlterationsResponse
{
    public List<CustomerRequestDto> CustomerRequests { get; set; }
}