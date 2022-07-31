namespace Suitsupply.Alteration.Domain.CustomerRequestAggregate;

public interface ICustomerRequestRepository
{
    Task<List<CustomerRequest>> GetAllCustomerRequestsAsync(string shopId);
    
    Task<CustomerRequest> GetCustomerRequestByIdAsync(string id, string shopId);

    Task<bool> UpdateCustomerRequestToPaidAsync(string id, string shopId, DateTime paidAt);
    
    Task<bool> FinishCustomerRequestAsync(string id, string shopId, DateTime finishedAt);

    Task<Guid> SendCustomerRequestAsync(CustomerRequest customerRequest);
}