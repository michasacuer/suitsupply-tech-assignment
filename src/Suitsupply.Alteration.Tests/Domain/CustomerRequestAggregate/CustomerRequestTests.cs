using Suitsupply.Alteration.Common.Exceptions;
using Suitsupply.Alteration.Domain.AlterationAggregate;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;
using Suitsupply.Alteration.Tests.Infrastructure;

namespace Suitsupply.Alteration.Tests.Domain.CustomerRequestAggregate;

public class CustomerRequestTests
{
    [Theory]
    [NoRecurseAutoData]
    public void CustomerRequest_Paid_PaidAt_CantBeSmaller_Than_CreatedAt(string shopId, string customerName, DateTime createdAt)
    {
        // Arrange:
        string customerEmail = "abcdewfg@wp.pl";
        DateTime smallerThanCreatedAt = new DateTime(createdAt.Ticks).AddHours(-10);
        var alterationInfo = new ShortenInfo(1, 1, 1, 1);
        
        var request = new CustomerRequest(shopId, customerName, customerEmail, alterationInfo, createdAt, string.Empty);
        
        // Act && Assert:
        Assert.Throws<SuitsupplyBusinessException>(() => request.Paid(smallerThanCreatedAt));
    }
    
    [Theory]
    [NoRecurseAutoData]
    public void CustomerRequest_Finished_FinishedAt_CantBeSmaller_Than_CreatedAt(string shopId, string customerName, DateTime createdAt)
    {
        // Arrange:
        string customerEmail = "abcdewfg@wp.pl";
        DateTime smallerThanCreatedAt = new DateTime(createdAt.Ticks).AddHours(-10);
        var alterationInfo = new ShortenInfo(1, 1, 1, 1);
        
        var request = new CustomerRequest(shopId, customerName, customerEmail, alterationInfo, createdAt, string.Empty);
        
        // Act && Assert:
        Assert.Throws<SuitsupplyBusinessException>(() => request.Finished(smallerThanCreatedAt));
    }
    
    [Theory]
    [NoRecurseAutoData]
    public void CustomerRequest_Paid_CanNot_SetPaidAtToTrue_WhenRequestAlreadyPaid(CustomerRequest request)
    {
        // Arrange:
        request.IsPaid = true;
        
        // Act && Assert:
        Assert.Throws<SuitsupplyBusinessException>(() => request.Paid(request.CreatedAt.AddHours(1)));
    }
    
    [Theory]
    [NoRecurseAutoData]
    public void CustomerRequest_Paid_CanNot_SetPaidAtToTrue_When_IsPaidOrNotAccepted(CustomerRequest request)
    {
        // Arrange:
        request.IsPaid = true;
        request.Status = RequestStatus.Started;
        
        // Act && Assert:
        Assert.Throws<SuitsupplyBusinessException>(() => request.Paid(request.CreatedAt.AddHours(1)));
    }
    
    [Theory]
    [NoRecurseAutoData]
    public void CustomerRequest_Paid_SetToTrue_WhenStatusAccepted_IsPaidToFalse_DateCorrect(CustomerRequest request)
    {
        // Arrange:
        request.IsPaid = false;
        request.Status = RequestStatus.Accepted;
        request.PaidAt = null;
        var date = request.CreatedAt.AddHours(1);
        
        // Act:
        request.Paid(date);
        
        // Assert:
        Assert.True(request.IsPaid);
        Assert.Equal(RequestStatus.Started, request.Status);
        Assert.NotNull(request.PaidAt);
    }
    
    [Theory]
    [NoRecurseAutoData]
    public void CustomerRequest_Finished_SetToTrue_WhenStatusStarted_IsPaidToTrue_DateCorrect(CustomerRequest request)
    {
        // Arrange:
        request.IsPaid = true;
        request.Status = RequestStatus.Started;
        var date = request.CreatedAt.AddHours(1);
        
        // Act:
        request.Finished(date);
        
        // Assert:
        Assert.Equal(RequestStatus.Finished, request.Status);
        Assert.NotNull(request.FinishedAt);
    }
    
    [Theory]
    [NoRecurseAutoData]
    public void CustomerRequest_Finished_CanNot_SetPaidAtToTrue_When_IsNotPaidOrNotStarted(CustomerRequest request)
    {
        // Arrange:
        request.IsPaid = false;
        request.Status = RequestStatus.Accepted;
        
        // Act && Assert:
        Assert.Throws<SuitsupplyBusinessException>(() => request.Finished(request.CreatedAt.AddHours(1)));
    }
}