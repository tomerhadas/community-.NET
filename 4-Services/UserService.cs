using AutoMapper;
using CommunityEventHub.DAL;
using CommunityEventHub.Models;
using CommunityEventHub.Models.Dto;

namespace CommunityEventHub.Services;

public class UserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _repo.GetAllAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        if (await _repo.ExistsByEmailAsync(dto.Email))
            throw new Exception("Email already exists");

        var user = _mapper.Map<User>(dto);
        await _repo.AddAsync(user);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateAsync(int id, CreateUserDto dto)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) return null;
        user.FullName = dto.FullName;
        user.Email = dto.Email;
        await _repo.UpdateAsync(user);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) return false;
        await _repo.DeleteAsync(id);
        return true;
    }
}
