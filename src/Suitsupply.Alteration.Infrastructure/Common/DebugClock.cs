using Suitsupply.Alteration.Common.Interfaces;

namespace Suitsupply.Alteration.Infrastructure.Common;

public class DebugClock : IClock
{
    private TimeSpan _offset = new TimeSpan();

    public DateTime Now => DateTime.UtcNow + _offset;

    public void SetNow(DateTime date) =>_offset = date - DateTime.UtcNow;

    public void Reset() => _offset = new TimeSpan();
}