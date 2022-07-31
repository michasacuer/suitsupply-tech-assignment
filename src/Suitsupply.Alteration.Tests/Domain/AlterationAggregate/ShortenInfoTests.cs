using Suitsupply.Alteration.Common.Exceptions;
using Suitsupply.Alteration.Domain.AlterationAggregate;

namespace Suitsupply.Alteration.Tests.Domain.AlterationAggregate;

public class ShortenInfoTests
{
    [Theory]
    [InlineData(1, 2, 3, 4)]
    [InlineData(4, 3, 2, 1)]
    [InlineData(1, 1, 1, 1)]
    [InlineData(-1, -1, -1, -1)]
    [InlineData(
        ShortenInfo.MaxAlternationDifferenceInCentimeters,
        ShortenInfo.MaxAlternationDifferenceInCentimeters,
        ShortenInfo.MaxAlternationDifferenceInCentimeters,
        ShortenInfo.MaxAlternationDifferenceInCentimeters)]
    [InlineData(
        -ShortenInfo.MaxAlternationDifferenceInCentimeters,
        -ShortenInfo.MaxAlternationDifferenceInCentimeters,
        -ShortenInfo.MaxAlternationDifferenceInCentimeters,
        -ShortenInfo.MaxAlternationDifferenceInCentimeters)]
    public void ShortenInfo_Ctor_DataValid_ObjectCreated(
        int leftTrouserLeg,
        int rightTrouserLeg,
        int leftSleeve,
        int rightSleeve)
    {
        // Arrange & Act:
        var o = new ShortenInfo(leftTrouserLeg, rightTrouserLeg, leftSleeve, rightSleeve);
        
        // Assert:
        Assert.NotNull(o);
    }
    
    [Theory]
    [InlineData(
        ShortenInfo.MaxAlternationDifferenceInCentimeters + 1,
        ShortenInfo.MaxAlternationDifferenceInCentimeters + 5,
        ShortenInfo.MaxAlternationDifferenceInCentimeters + 4,
        ShortenInfo.MaxAlternationDifferenceInCentimeters + 3)]
    [InlineData(
        -ShortenInfo.MaxAlternationDifferenceInCentimeters - 5,
        -ShortenInfo.MaxAlternationDifferenceInCentimeters - 2,
        -ShortenInfo.MaxAlternationDifferenceInCentimeters - 3,
        -ShortenInfo.MaxAlternationDifferenceInCentimeters - 1)]
    public void ShortenInfo_Ctor_AlternationsBiggerThanMax_ThrowNew_SuitSupplyBusinessException(
        int leftTrouserLeg,
        int rightTrouserLeg,
        int leftSleeve,
        int rightSleeve)
    {
        // Arrange & Act & Assert:
        Assert.Throws<SuitsupplyBusinessException>(
            () => new ShortenInfo(leftTrouserLeg, rightTrouserLeg, leftSleeve, rightSleeve));
    }
    
    [Fact]
    public void ShortenInfo_Ctor_AlternationsDefaultValues_ThrowNew_SuitSupplyBusinessException()
    {
        // Arrange & Act & Assert:
        Assert.Throws<SuitsupplyBusinessException>(() => new ShortenInfo(0, 0, 0, 0));
    }
}