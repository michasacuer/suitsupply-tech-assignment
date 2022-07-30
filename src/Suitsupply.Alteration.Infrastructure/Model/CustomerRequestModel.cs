using Azure;
using Azure.Data.Tables;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;

namespace Suitsupply.Alteration.Infrastructure.Model;

public class CustomerRequestModel : CustomerRequest, ITableEntity
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