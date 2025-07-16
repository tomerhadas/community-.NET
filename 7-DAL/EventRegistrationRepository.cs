using CommunityEventHub.Data;
using CommunityEventHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventHub.DAL
{
    public class EventRegistrationRepository : IEventRegistrationRepository
    {
        private readonly CommunityEventHubContext _context;

        public EventRegistrationRepository(CommunityEventHubContext context)
        {
            _context = context;
        }

        public async Task<EventRegistration?> GetByIdAsync(int id) =>
            await _context.EventRegistrations.FindAsync(id);

        public async Task<List<EventRegistration>> GetAllAsync() =>
            await _context.EventRegistrations.ToListAsync();

        public async Task AddAsync(EventRegistration reg)
        {
            _context.EventRegistrations.Add(reg);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reg = await GetByIdAsync(id);
            if (reg != null)
            {
                _context.EventRegistrations.Remove(reg);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int userId, int eventId) =>
            await _context.EventRegistrations.AnyAsync(r => r.UserId == userId && r.EventId == eventId);
    }
}
