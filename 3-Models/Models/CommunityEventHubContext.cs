using CommunityEventHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CommunityEventHub.Data;

public class CommunityEventHubContext : DbContext
{
    public CommunityEventHubContext(DbContextOptions<CommunityEventHubContext> options)
        : base(options)
    {
    }

    // DbSets (טבלאות)
    public DbSet<User> Users { get; set; }
    public DbSet<EventBase> Events { get; set; }
    public DbSet<Meetup> Meetups { get; set; }
    public DbSet<Webinar> Webinars { get; set; }
    public DbSet<Conference> Conferences { get; set; }
    public DbSet<JobFair> JobFairs { get; set; }
    public DbSet<EventRegistration> EventRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Table-per-Hierarchy (TPH) לאירועים – EF Core ברירת מחדל
        modelBuilder.Entity<EventBase>()
            .HasDiscriminator<string>("EventType")
            .HasValue<Meetup>("Meetup")
            .HasValue<Webinar>("Webinar")
            .HasValue<Conference>("Conference")
            .HasValue<JobFair>("JobFair");

        // קשרים והרשאות
        modelBuilder.Entity<EventRegistration>()
            .HasOne(er => er.User)
            .WithMany(u => u.EventRegistrations)
            .HasForeignKey(er => er.UserId);

        modelBuilder.Entity<EventRegistration>()
            .HasOne(er => er.Event)
            .WithMany(e => e.EventRegistrations)
            .HasForeignKey(er => er.EventId);

        // הגדרות נוספות (אופציונלי): אילוצים, אורך שדות, ייחודיות וכו'
    }
}
