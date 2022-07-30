namespace Suitsupply.Alteration.Domain.CustomerRequestAggregate;

public interface ICustomerRequestRepository
{
    Task<List<CustomerRequest>> GetAllCustomerRequestsAsync(string shopId);
    
    Task<CustomerRequest> GetCustomerRequestByIdAsync(Guid id, string shopId);

    Task UpdateCustomerRequestAsync(CustomerRequest request);

    Task<bool> SendCustomerRequestAsync(CustomerRequest request);
}