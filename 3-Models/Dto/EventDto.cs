using System;

namespace CommunityEventHub.Models.Dto
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string Location { get; set; } = null!;
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string EventType { get; set; } = null!;
    }
}
