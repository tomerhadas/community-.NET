using CommunityEventHub.Models;

namespace CommunityEventHub.DAL;

public interface IEventRepository
{
    Task<EventBase?> GetByIdAsync(int id);
    Task<List<EventBase>> GetAllAsync();
    Task AddAsync(EventBase ev);
    Task UpdateAsync(EventBase ev);
    Task DeleteAsync(int id);
}
