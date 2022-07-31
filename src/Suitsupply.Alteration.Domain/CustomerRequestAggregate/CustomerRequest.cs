using Suitsupply.Alteration.Common.Exceptions;
using Suitsupply.Alteration.Common.Interfaces;
using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Domain.AlterationAggregate;

namespace Suitsupply.Alteration.Domain.CustomerRequestAggregate;

public class CustomerRequest : IBaseEntity<Guid>
{
    protected CustomerRequest()
    {
    }
    
    public CustomerRequest(
        string shopId,
        string customerName,
        string customerEmail,
        IAlterationInfo alterationInfo,
        DateTime createdAt,
        string payload)
    {
        Ensure.NotNull(alterationInfo, nameof(alterationInfo));
        Ensure.StringNotNullOrEmpty(shopId, nameof(shopId));
        Ensure.StringNotNullOrEmpty(customerName, nameof(customerName));
        Ensure.StringNotNullOrEmpty(customerEmail, nameof(customerEmail));
        Ensure.DateTimeNotEmpty(createdAt, nameof(createdAt));

        ShopId = shopId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CreatedAt = createdAt;
        InfoForTailors = alterationInfo.MessageForTailors();
        Payload = payload;
    }
    
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string ShopId { get; set; }

    public string CustomerName { get; init; }

    public string CustomerEmail { get; init; }
    
    public string InfoForTailors { get; init; }

    public string Payload { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? FinishedAt { get; set; }

    public DateTime? PaidAt { get; set; }
    
    public bool IsPaid { get; set; }

    public RequestStatus Status { get; set; } = RequestStatus.Accepted;

    public void Paid(DateTime paidAt)
    {
        if (IsPaid || Status != RequestStatus.Accepted)
        {
            throw new SuitsupplyBusinessException(ErrorMessages.PAID_OR_WRONG_STATUS);
        }
        
        Ensure.DateTimeNotEmpty(paidAt, nameof(paidAt));
        CheckIfDateComparedWithCreatedIsValid(paidAt);
        
        IsPaid = true;
        Status = RequestStatus.Started;
        PaidAt = paidAt;
    }

    public void Finished(DateTime finishedAt)
    {
        if (!IsPaid || Status != RequestStatus.Started)
        {
            throw new SuitsupplyBusinessException(ErrorMessages.FINISHED_OR_WRONG_STATUS);
        }
        
        Ensure.DateTimeNotEmpty(finishedAt, nameof(finishedAt));
        CheckIfDateComparedWithCreatedIsValid(finishedAt);
        
        Status = RequestStatus.Finished;
        FinishedAt = finishedAt;
    }

    private void CheckIfDateComparedWithCreatedIsValid(DateTime date)
    {
        if (date < CreatedAt)
        {
            throw new SuitsupplyBusinessException(ErrorMessages.DATE_CAN_NOT_BE_BIGGER);
        }
    }
}