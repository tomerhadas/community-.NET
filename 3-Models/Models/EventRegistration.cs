using System;

namespace CommunityEventHub.Models;

public class EventRegistration
{
    public int Id { get; set; }

    // Foreign Keys
    public int UserId { get; set; }
    public int EventId { get; set; }

    public DateTime RegistrationDate { get; set; }
    public string Status { get; set; } // e.g. Pending, Approved, Rejected

    // Navigation Properties
    public User User { get; set; } = null!;
    public EventBase Event { get; set; } = null!;
}
