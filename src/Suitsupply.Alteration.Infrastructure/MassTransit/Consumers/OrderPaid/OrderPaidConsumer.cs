using MassTransit;
using Suitsupply.Alteration.Common.Exceptions;
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
        Ensure.StringNotNullOrEmpty(message.ShopId, nameof(message.ShopId));
        Ensure.DateTimeNotEmpty(message.PaidAt, nameof(message.PaidAt));
        
        bool isUpdated = await _customerRequestRepository.UpdateCustomerRequestToPaidAsync(message.Id, message.ShopId, message.PaidAt);
        if (!isUpdated)
        {
            throw new SuitsupplyBusinessException("Can't update customer's request.");
        }
    }
}