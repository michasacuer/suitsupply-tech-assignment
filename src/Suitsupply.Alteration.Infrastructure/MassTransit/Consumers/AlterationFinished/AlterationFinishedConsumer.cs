using MassTransit;
using Suitsupply.Alteration.Common.Exceptions;
using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;
using Suitsupply.Alteration.Infrastructure.EmailSender;

namespace Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.AlterationFinished;

public class AlterationFinishedConsumer : IConsumer<AlterationFinishedMessage>
{
    private readonly ICustomerRequestRepository _customerRequestRepository;
    private readonly IEmailService _emailService;

    public AlterationFinishedConsumer(ICustomerRequestRepository customerRequestRepository, IEmailService emailService)
    {
        _customerRequestRepository = customerRequestRepository ?? throw new ArgumentNullException(nameof(customerRequestRepository));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    public async Task Consume(ConsumeContext<AlterationFinishedMessage> context)
    {
        var message = context.Message;
        Ensure.StringNotNullOrEmpty(message.Id, nameof(message.Id));
        Ensure.StringNotNullOrEmpty(message.ShopId, nameof(message.ShopId));
        Ensure.DateTimeNotEmpty(message.FinishedAt, nameof(message.FinishedAt));

        bool isUpdated = await _customerRequestRepository.FinishCustomerRequestAsync(message.Id, message.ShopId, message.FinishedAt);
        if(!isUpdated)
        {
            throw new SuitsupplyBusinessException("Can't update entity.");
        }

        var customerRequest = await _customerRequestRepository.GetCustomerRequestByIdAsync(message.Id, message.ShopId);
        
        await _emailService.SendEmailAsync(customerRequest.CustomerEmail, $"Dear {customerRequest.CustomerName} your order is finished.", "Alteration finished!");
    }
}