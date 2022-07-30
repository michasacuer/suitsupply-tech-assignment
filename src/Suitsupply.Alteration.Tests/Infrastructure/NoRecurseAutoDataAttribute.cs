using AutoFixture.Xunit2;

namespace Suitsupply.Alteration.Tests.Infrastructure;

public sealed class NoRecurseAutoDataAttribute : AutoDataAttribute
{
    public NoRecurseAutoDataAttribute(int repeatCount = 1)
#pragma warning disable 618
        : base(MoqFixture.Create(repeatCount))
#pragma warning restore 618
    {
    }
}