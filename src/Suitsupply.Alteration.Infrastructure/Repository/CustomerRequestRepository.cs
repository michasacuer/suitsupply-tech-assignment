using Azure;
using Azure.Data.Tables;
using Suitsupply.Alteration.Common.Exceptions;
using Suitsupply.Alteration.Common.Utils;
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
        
        return response.OrderByDescending(x => x.CreatedAt).ToList();
    }

    public async Task<CustomerRequest> GetCustomerRequestByIdAsync(string id, string shopId)
        => await GetEntityAsync(id, shopId);

    public async Task<bool> UpdateCustomerRequestToPaidAsync(string id, string shopId, DateTime paidAt)
    {
        var customerRequest = await GetEntityAsync(id, shopId);
        
        customerRequest.Paid(paidAt);
        var result = await _tableClient.UpdateEntityAsync(customerRequest, ETag.All);

        return !result.IsError;
    }

    public async Task<bool> FinishCustomerRequestAsync(string id, string shopId, DateTime finishedAt)
    {
        var customerRequest = await GetEntityAsync(id, shopId);

        customerRequest.Finished(finishedAt);
        var result = await _tableClient.UpdateEntityAsync(customerRequest, ETag.All);

        return !result.IsError;
    }

    public async Task<Guid> SendCustomerRequestAsync(CustomerRequest customerRequest)
    {
        var customerRequestModel = new CustomerRequestModel(customerRequest);
        var result = await _tableClient.AddEntityAsync(customerRequestModel);

        if (result.IsError)
        {
            throw new SuitsupplyBusinessException();
        }

        return customerRequestModel.Id;
    }
    
    private async Task<CustomerRequestModel> GetEntityAsync(string id, string shopId)
    {
        try
        {
            var response = await _tableClient.GetEntityAsync<CustomerRequestModel>(shopId, id);

            var customerRequest = response.Value;
            Ensure.NotNull(customerRequest, nameof(customerRequest));
        
            return customerRequest;
        }
        catch (RequestFailedException)
        {
            throw new SuitsupplyBusinessException(ErrorMessages.CUSTOMER_REQUEST_NOT_FOUND);
        }
    }
}