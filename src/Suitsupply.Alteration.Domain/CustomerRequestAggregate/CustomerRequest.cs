using Suitsupply.Alteration.Common.Interfaces;
using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Domain.AlterationAggregate;

namespace Suitsupply.Alteration.Domain.CustomerRequestAggregate;

public class CustomerRequest : IBaseEntity<Guid>
{
    public CustomerRequest(
        string customerName,
        string customerEmail,
        IAlterationInfo alterationInfo,
        DateTime createdAt,
        string payload)
    {
        Ensure.NotNull(alterationInfo, nameof(alterationInfo));
        Ensure.StringNotNullOrEmpty(customerName, nameof(customerName));
        Ensure.StringNotNullOrEmpty(customerEmail, nameof(customerEmail));
        Ensure.DateTimeNotEmpty(createdAt, nameof(createdAt));
        
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CreatedAt = createdAt;
        InfoForTailors = alterationInfo.MessageForTailors();
        Payload = payload;
    }

    public Guid Id { get; } = Guid.NewGuid();

    public string CustomerName { get; }

    public string CustomerEmail { get; }
    
    public string InfoForTailors { get; }

    public string Payload { get; }

    public DateTime CreatedAt { get; }

    public DateTime? FinishedAt { get; private set; }

    public DateTime? PaidAt { get; private set; }
    
    public bool IsPaid { get; private set; }

    public RequestStatus Status { get; private set; } = RequestStatus.Accepted;

    public void Paid(DateTime paidAt)
    {
        Ensure.DateTimeNotEmpty(paidAt, nameof(paidAt));
        
        IsPaid = true;
        Status = RequestStatus.Started;
        PaidAt = paidAt;
    }

    public void Finished(DateTime finishedAt)
    {
        Ensure.DateTimeNotEmpty(finishedAt, nameof(finishedAt));
        
        Status = RequestStatus.Finished;
        FinishedAt = finishedAt;
    }
}