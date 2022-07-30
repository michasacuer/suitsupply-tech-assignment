using MassTransit;
using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;

namespace Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.OrderPaid;

public class OrderPaidConsumer : IConsumer<OrderPaidMessage>
{
    private readonly ICustomerRequestRepository _customerRequestRepository;

    public OrderPaidConsumer(ICustomerRequestRepository customerRequestRepository)
    {
        _customerRequestRepository = customerRequestRepository ?? throw new ArgumentNullException(nameof(customerRequestRepository));
    }

    public async Task Consume(ConsumeContext<OrderPaidMessage> context)
    {
        var message = context.Message;
        Ensure.StringNotNullOrEmpty(message.Id, nameof(message.Id));
        Ensure.StringNotNullOrEmpty(message.Id, nameof(message.Id));
        Ensure.DateTimeNotEmpty(message.PaidAt, nameof(message.PaidAt));

        var customerRequest = await _customerRequestRepository.GetCustomerRequestByIdAsync(new Guid(message.Id), message.ShopId);
        Ensure.NotNull(customerRequest, nameof(customerRequest));
        
        customerRequest.Paid(message.PaidAt);
    }
}