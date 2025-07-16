using AutoMapper;
using CommunityEventHub.DAL;
using CommunityEventHub.Models;
using CommunityEventHub.Models.Dto;

namespace CommunityEventHub.Services
{
    public class EventService
    {
        private readonly IEventRepository _repo;
        private readonly IMapper _mapper;

        public EventService(IEventRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<EventDto?> GetByIdAsync(int id)
        {
            var ev = await _repo.GetByIdAsync(id);
            return ev == null ? null : _mapper.Map<EventDto>(ev);
        }

        public async Task<List<EventDto>> GetAllAsync()
        {
            var events = await _repo.GetAllAsync();
            return _mapper.Map<List<EventDto>>(events);
        }

        public async Task<EventDto> CreateAsync(CreateEventDto dto)
        {
            var ev = _mapper.Map<EventBase>(dto);
            await _repo.AddAsync(ev);
            return _mapper.Map<EventDto>(ev);
        }

        public async Task<EventDto?> UpdateAsync(int id, CreateEventDto dto)
        {
            var ev = await _repo.GetByIdAsync(id);
            if (ev == null) return null;
            _mapper.Map(dto, ev); // מעדכן את השדות
            await _repo.UpdateAsync(ev);
            return _mapper.Map<EventDto>(ev);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ev = await _repo.GetByIdAsync(id);
            if (ev == null) return false;
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
