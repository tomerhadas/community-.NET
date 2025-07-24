using AutoMapper;
using CommunityEventHub.DAL;
using CommunityEventHub.Models;
using CommunityEventHub.Models.Dto;

namespace CommunityEventHub.Services;

public class EventRegistrationService
{
    private readonly IEventRegistrationRepository _repo;
    private readonly IMapper _mapper;

    public EventRegistrationService(IEventRegistrationRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<EventRegistrationDto>> GetAllAsync()
    {
        var regs = await _repo.GetAllAsync();
        return _mapper.Map<List<EventRegistrationDto>>(regs);
    }

    public async Task<EventRegistrationDto?> GetByIdAsync(int id)
    {
        var reg = await _repo.GetByIdAsync(id);
        return reg == null ? null : _mapper.Map<EventRegistrationDto>(reg);
    }

    public async Task<EventRegistrationDto> CreateAsync(CreateEventRegistrationDto dto)
    {
        if (await _repo.ExistsAsync(dto.UserId, dto.EventId))
            throw new Exception("User already registered for this event.");

        var reg = _mapper.Map<EventRegistration>(dto);
        await _repo.AddAsync(reg);
        return _mapper.Map<EventRegistrationDto>(reg);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var reg = await _repo.GetByIdAsync(id);
        if (reg == null) return false;
        await _repo.DeleteAsync(id);
        return true;
    }
}
