using Suitsupply.Alteration.Common.Interfaces;

namespace Suitsupply.Alteration.Infrastructure.Common;

public class Clock : IClock
{
    public DateTime Now => DateTime.UtcNow;
}