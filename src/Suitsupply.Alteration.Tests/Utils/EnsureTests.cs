using Suitsupply.Alteration.Common.Utils;
using Suitsupply.Alteration.Tests.Infrastructure;

namespace Suitsupply.Alteration.Tests.Utils;

public class EnsureTests
{
    [Fact]
    public void Ensure_NotNull_ThrowsArgumentNullException_WhenObjectIsNull()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(null, string.Empty));
    }
    
    [Fact]
    public void Ensure_NotNull_DoNothing_WhenObjectIsNotNull()
    {
        // Arrange & Act & Assert
        Ensure.NotNull(new object(), string.Empty);
    }
    
    [Fact]
    public void Ensure_DateTimeNotEmpty_ThrowsArgumentNullException_WhenDateTimeIsMinValue()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => Ensure.DateTimeNotEmpty(DateTime.MinValue, string.Empty));
    }
    
    [Theory]
    [NoRecurseAutoData]
    public void Ensure_DateTimeNotEmpty_DoNothing_WhenDateTimeIsDifferentThanMinValue(DateTime date)
    {
        // Arrange & Act & Assert
        Ensure.DateTimeNotEmpty(date, string.Empty);
    }
    
    [Fact]
    public void Ensure_StringNotNullOrEmpty_ThrowsArgumentNullException_WhenStringIsEmpty()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => Ensure.StringNotNullOrEmpty(string.Empty, string.Empty));
    }
    
    [Fact]
    public void Ensure_StringNotNullOrEmpty_ThrowsArgumentNullException_WhenStringIsNull()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => Ensure.StringNotNullOrEmpty(null, string.Empty));
    }
    
    [Theory]
    [NoRecurseAutoData]
    public void Ensure_StringNotNullOrEmpty_DoNothing_WhenStringExist(string s)
    {
        // Arrange & Act & Assert
        Ensure.StringNotNullOrEmpty(s, string.Empty);
    }
}