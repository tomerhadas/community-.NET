using CommunityEventHub.Models;

namespace CommunityEventHub.DAL
{
    public interface IEventRegistrationRepository
    {
        Task<EventRegistration?> GetByIdAsync(int id);
        Task<List<EventRegistration>> GetAllAsync();
        Task AddAsync(EventRegistration reg);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int userId, int eventId);
    }
}
