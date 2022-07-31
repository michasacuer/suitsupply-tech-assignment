using Suitsupply.Alteration.Api.Commands.SendCustomerRequest;
using Suitsupply.Alteration.Api.Services;
using Suitsupply.Alteration.Common.Exceptions;
using Suitsupply.Alteration.Common.Interfaces;
using Suitsupply.Alteration.Domain.AlterationAggregate;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;
using Suitsupply.Alteration.Tests.Infrastructure;

namespace Suitsupply.Alteration.Tests.Api.Commands;

public class SendCustomerRequestCommandHandlerTests
{
    private static string _appId = "1";
    private readonly Mock<ICustomerRequestRepository> _customerRequestRepositoryMock = new();
    private readonly Mock<IHttpContextFacade> _httpContextAccessorMock = new();
    private readonly Mock<IClock> _clockMock = new();
    
    private readonly SendCustomerRequestCommandHandler _uut;
    
    public SendCustomerRequestCommandHandlerTests()
    {
        _uut = new SendCustomerRequestCommandHandler(
            _customerRequestRepositoryMock.Object,
            _httpContextAccessorMock.Object,
            _clockMock.Object);

        _clockMock.SetupGet(x => x.Now).Returns(DateTime.Now);
        _httpContextAccessorMock.Setup(x => x.GetAppIdFromClaim()).Returns(_appId);
    }

    [Fact]
    public void SendCustomerRequestCommandHandler_Handle_CorrectDto_ReturnUnitValue()
    {
        // Arrange:
        string name = "name";
        string email = "email";
        int changeSizeBy = 1;

        var request = new SendCustomerRequestCommandDto
        {
            CustomerName = name,
            CustomerEmail = email,
            LeftSleeveShortenBy = changeSizeBy
        };
        
        CustomerRequest sendCustomerRequest = null;
        _customerRequestRepositoryMock
            .Setup(x => x.SendCustomerRequestAsync(It.IsAny<CustomerRequest>()))
            .Callback<CustomerRequest>(x => sendCustomerRequest = x)
            .ReturnsAsync(new Guid());
        
        // Act:
        _ = _uut.Handle(request, CancellationToken.None).Result;
        
        // Assert:
        Assert.NotNull(sendCustomerRequest);
        Assert.Equal(_appId, sendCustomerRequest.ShopId);
        Assert.Equal(name, sendCustomerRequest.CustomerName);
        Assert.Equal(email, sendCustomerRequest.CustomerEmail);
    }
    
    [Theory]
    [NoRecurseAutoData]
    public void SendCustomerRequestCommandHandler_Handle_NoCorrectionsProvided_Throws_SuitSupplyBusinessException(int cm)
    {
        // Arrange:
        string name = "name";
        string email = "email";
        int changeSizeBy = ShortenInfo.MaxAlternationDifferenceInCentimeters + cm;

        var request = new SendCustomerRequestCommandDto
        {
            CustomerName = name,
            CustomerEmail = email,
            LeftSleeveShortenBy = changeSizeBy
        };
        
        // Act & Assert:
        Assert.Throws<AggregateException>(() => _uut.Handle(request, CancellationToken.None).Result);
    }   
}