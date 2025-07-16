using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityEventHub.Models;


public class User
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;


    public ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();
}
