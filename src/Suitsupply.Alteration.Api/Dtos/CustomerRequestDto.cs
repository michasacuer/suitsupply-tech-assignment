using Suitsupply.Alteration.Domain.CustomerRequestAggregate;

namespace Suitsupply.Alteration.Api.Dtos;

public class CustomerRequestDto
{
    public Guid Id { get; set; } 

    public string CustomerName { get; set; } 

    public string CustomerEmail { get; set; } 
    
    public string InfoForTailors { get; set; } 

    public string Payload { get; set; } 

    public DateTime CreatedAt { get; set; } 

    public DateTime? FinishedAt { get; set; } 

    public DateTime? PaidAt { get; set; } 
    
    public bool IsPaid { get; set; } 

    public RequestStatus Status { get; set; } 
}