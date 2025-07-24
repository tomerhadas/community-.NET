using CommunityEventHub.Models;

public class EventRegistration
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int EventId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string Status { get; set; } = "Registered";
    public User User { get; set; }
    public EventBase Event { get; set; }
}
