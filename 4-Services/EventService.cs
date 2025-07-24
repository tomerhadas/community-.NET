using AutoMapper;
using CommunityEventHub.DAL;
using CommunityEventHub.Models;
using CommunityEventHub.Models.Dto;

namespace CommunityEventHub.Services;

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
        EventBase ev;

        switch (dto.EventType?.ToLower())
        {
            case "meetup":
                ev = _mapper.Map<Meetup>(dto);
                break;
            case "webinar":
                ev = _mapper.Map<Webinar>(dto);
                break;
            case "conference":
                ev = _mapper.Map<Conference>(dto);
                break;
            case "jobfair":
                ev = _mapper.Map<JobFair>(dto);
                break;
            default:
                throw new ArgumentException("Invalid or missing eventType", nameof(dto.EventType));
        }

        await _repo.AddAsync(ev);
        return _mapper.Map<EventDto>(ev);
    }

    public async Task<EventDto?> UpdateAsync(int id, CreateEventDto dto)
    {
        var ev = await _repo.GetByIdAsync(id);
        if (ev == null) return null;

        // שדרוג: ניתן למפות לפי הסוג הקונקרטי
        switch (ev.GetEventType().ToLower())
        {
            case "meetup":
                _mapper.Map(dto, (Meetup)ev);
                break;
            case "webinar":
                _mapper.Map(dto, (Webinar)ev);
                break;
            case "conference":
                _mapper.Map(dto, (Conference)ev);
                break;
            case "jobfair":
                _mapper.Map(dto, (JobFair)ev);
                break;
            default:
                throw new ArgumentException("Invalid eventType in database");
        }

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
