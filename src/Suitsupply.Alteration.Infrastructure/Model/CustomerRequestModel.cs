using Azure;
using Azure.Data.Tables;
using Suitsupply.Alteration.Domain.AlterationAggregate;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;

namespace Suitsupply.Alteration.Infrastructure.Model;

public record CustomerRequestModel : CustomerRequest, ITableEntity
{
    public CustomerRequestModel()
    {
    }
    
    public CustomerRequestModel(CustomerRequest request)
    {
        ShopId = request.ShopId;
        CustomerName = request.CustomerName;
        CustomerEmail = request.CustomerEmail;
        CreatedAt = request.CreatedAt;
        InfoForTailors = request.InfoForTailors;
        Payload = request.Payload;
        Status = request.Status;
        IsPaid = request.IsPaid;
        PaidAt = request.PaidAt;
        FinishedAt = request.FinishedAt;
    }

    public CustomerRequestModel(
        string shopId,
        string customerName,
        string customerEmail,
        IAlterationInfo alterationInfo,
        DateTime createdAt,
        string payload)
        : base(shopId, customerName, customerEmail, alterationInfo, createdAt, payload)
    {
    }

    public string PartitionKey
    {
        get => ShopId;
        set => ShopId = value;
    }

    public string RowKey
    {
        get => Id.ToString();
        set => Id = new Guid(value);
    }
    
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }
}