using AutoFixture;
using AutoFixture.AutoMoq;

namespace Suitsupply.Alteration.Tests.Infrastructure;

public static class MoqFixture
{
    public static IFixture Create(int repeatCount)
    {
        var fixture = new Fixture
        {
            RepeatCount = repeatCount
        };

        fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        fixture.Customize(new AutoMoqCustomization { ConfigureMembers = false });

        return fixture;
    }
}