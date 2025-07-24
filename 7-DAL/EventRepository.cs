using CommunityEventHub.Models;
using CommunityEventHub.Data;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventHub.DAL;

public class EventRepository : IEventRepository
{
    private readonly CommunityEventHubContext _context;

    public EventRepository(CommunityEventHubContext context)
    {
        _context = context;
    }

    public async Task<EventBase?> GetByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task<List<EventBase>> GetAllAsync()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task AddAsync(EventBase ev)
    {
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EventBase ev)
    {
        _context.Events.Update(ev);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var ev = await GetByIdAsync(id);
        if (ev != null)
        {
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
        }
    }
}
