namespace CommunityEventHub.Models;


public class JobFair : EventBase
{
    public string EmployersList { get; set; } = null!; 
    public int BoothsCount { get; set; }
    public bool HasResumeDrop { get; set; }

    public override string GetEventType() => "JobFair";
}
