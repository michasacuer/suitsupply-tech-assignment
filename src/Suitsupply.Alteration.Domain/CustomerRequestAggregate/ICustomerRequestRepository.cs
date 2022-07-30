namespace Suitsupply.Alteration.Domain.CustomerRequestAggregate;

public interface ICustomerRequestRepository
{
    Task<CustomerRequest> GetCustomerRequestByIdAsync(Guid id);
}