namespace CommunityEventHub.Models;


public class Meetup : EventBase
{

    public string Speakers { get; set; }

    public string Topic { get; set; }

    public int MaxParticipants { get; set; }

    public override string GetEventType() => "Meetup";
}
