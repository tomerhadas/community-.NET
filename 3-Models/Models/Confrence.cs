namespace CommunityEventHub.Models;


public class Conference : EventBase
{
    public string MainSpeaker { get; set; }
    public string Sponsors { get; set; } // Comma-separated or JSON string

    public override string GetEventType() => "Conference";
}
