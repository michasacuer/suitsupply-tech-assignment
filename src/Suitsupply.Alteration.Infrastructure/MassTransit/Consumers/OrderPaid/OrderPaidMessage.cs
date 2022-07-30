namespace Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.OrderPaid;

public class OrderPaidMessage
{
    public string Id { get; set; }
    
    public string ShopId { get; set; }

    public DateTime PaidAt { get; set; }
}