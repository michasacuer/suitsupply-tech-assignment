using Azure.Data.Tables;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;
using Suitsupply.Alteration.Infrastructure.Model;

namespace Suitsupply.Alteration.Infrastructure.Repository;

public class CustomerRequestRepository : ICustomerRequestRepository
{
    private readonly TableClient _tableClient;

    public CustomerRequestRepository(TableClient tableClient)
    {
        _tableClient = tableClient ?? throw new ArgumentNullException(nameof(tableClient));
    }

    public async Task<List<CustomerRequest>> GetAllCustomerRequestsAsync(string shopId)
    {
        List<CustomerRequest> response = new();
        await foreach (var entity in _tableClient.QueryAsync<CustomerRequestModel>(x => x.PartitionKey == shopId))
        {
            response.Add(entity);
        }

        return response;
    }

    public async Task<CustomerRequest> GetCustomerRequestByIdAsync(Guid id, string shopId)
        => _tableClient.GetEntity<CustomerRequestModel>(shopId, id.ToString());

    public Task UpdateCustomerRequest(CustomerRequest request)
    {
        throw new NotImplementedException();
    }
}