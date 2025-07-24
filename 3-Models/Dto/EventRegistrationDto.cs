namespace CommunityEventHub.Models.Dto;

public class EventRegistrationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int EventId { get; set; }
    public DateTime RegistrationDate { get; set; }
    // אופציונלי: public string Status { get; set; }
}
