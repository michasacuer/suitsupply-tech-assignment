namespace Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.AlterationFinished;

public class AlterationFinishedMessage
{
    public string Id { get; set; }

    public string ShopId { get; set; }

    public DateTime FinishedAt { get; set; }
}