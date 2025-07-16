using CommunityEventHub.Models;

public abstract class EventBase
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; } 
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string Location { get; set; }
    public bool IsActive { get; set; }
    public string ImageUrl { get; set; }

    public ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

    public abstract string GetEventType();
}
