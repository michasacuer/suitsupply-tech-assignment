﻿using Suitsupply.Alteration.Common.Exceptions;
using Suitsupply.Alteration.Common.Interfaces;
using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Domain.AlterationAggregate;

namespace Suitsupply.Alteration.Domain.CustomerRequestAggregate;

public record CustomerRequest : IBaseEntity<Guid>
{
    public CustomerRequest()
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
    
    public Guid Id { get; protected set; } = Guid.NewGuid();
    
    public string ShopId { get; protected set; }

    public string CustomerName { get; init; }

    public string CustomerEmail { get; init; }
    
    public string InfoForTailors { get; init; }

    public string Payload { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? FinishedAt { get; protected set; }

    public DateTime? PaidAt { get; protected set; }
    
    public bool IsPaid { get; protected set; }

    public RequestStatus Status { get; protected set; } = RequestStatus.Accepted;

    public void Paid(DateTime paidAt)
    {
        Ensure.DateTimeNotEmpty(paidAt, nameof(paidAt));
        CheckIfDateComparedWithCreatedIsValid(paidAt);
        
        IsPaid = true;
        Status = RequestStatus.Started;
        PaidAt = paidAt;
    }

    public void Finished(DateTime finishedAt)
    {
        Ensure.DateTimeNotEmpty(finishedAt, nameof(finishedAt));
        CheckIfDateComparedWithCreatedIsValid(finishedAt);
        
        Status = RequestStatus.Finished;
        FinishedAt = finishedAt;
    }

    private void CheckIfDateComparedWithCreatedIsValid(DateTime date)
    {
        if (date < CreatedAt)
        {
            throw new SuitsupplyBusinessException("Date can't be bigger than request creation date.");
        }
    }
}