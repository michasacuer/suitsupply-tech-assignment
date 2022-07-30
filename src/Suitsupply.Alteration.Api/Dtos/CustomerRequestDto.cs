using System.Text.Json.Serialization;

namespace Suitsupply.Alteration.Api.Dtos;

public class CustomerRequestDto
{
    public Guid Id { get; set; } 

    public string CustomerName { get; set; } 

    public string CustomerEmail { get; set; } 
    
    public string InfoForTailors { get; set; } 

    public string Payload { get; set; } 

    public DateTime CreatedAt { get; set; } 

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? FinishedAt { get; set; } 

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? PaidAt { get; set; } 
    
    public bool IsPaid { get; set; } 

    public string Status { get; set; } 
}