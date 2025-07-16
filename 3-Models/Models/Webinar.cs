using System.ComponentModel.DataAnnotations;

namespace CommunityEventHub.Models;

public class Webinar : EventBase
{
    [Required]
    public string OnlineLink { get; set; } = null!;

    public string? HostName { get; set; }

    public bool RequiresRegistration { get; set; } = true;

    public override string GetEventType() => "Webinar";
}
