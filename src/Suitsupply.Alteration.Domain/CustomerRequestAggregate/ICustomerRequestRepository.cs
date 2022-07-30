namespace Suitsupply.Alteration.Domain.CustomerRequestAggregate;

public interface ICustomerRequestRepository
{
    Task<List<CustomerRequest>> GetAllCustomerRequestsAsync(string shopId);
    
    Task<CustomerRequest> GetCustomerRequestByIdAsync(Guid id, string shopId);

    Task<bool> UpdateCustomerRequestToPaidAsync(string id, string shopId, DateTime paidAt);

    Task<bool> SendCustomerRequestAsync(CustomerRequest customerRequest);
}