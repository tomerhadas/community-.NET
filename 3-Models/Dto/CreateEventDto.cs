using System;

namespace CommunityEventHub.Models.Dto
{
    public class CreateEventDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string Location { get; set; } = null!;
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string EventType { get; set; } = null!; // סוג: Meetup/Webinar/Conference/JobFair
        // תוכל להוסיף כאן שדות ייחודיים לפי סוג
    }
}
