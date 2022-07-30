using Azure;
using Azure.Data.Tables;
using Suitsupply.Alteration.Domain.AlterationAggregate;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;

namespace Suitsupply.Alteration.Infrastructure.Model;

public class CustomerRequestModel : CustomerRequest, ITableEntity
{
    public CustomerRequestModel()
    {
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