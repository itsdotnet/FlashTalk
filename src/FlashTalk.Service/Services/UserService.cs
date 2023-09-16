using AutoMapper;
using FlashTalk.DataAccess.IRepositories;
using FlashTalk.Domain.Entities;
using FlashTalk.Service.DTOs.Users;
using FlashTalk.Service.Exceptions;

namespace FlashTalk.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> CheckUserAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (password.Equals(user.Password))
        {
            return true;
        }
        return false;
    }

    public async Task<UserResultDto> CreateAsync(UserCreationDto dto)
    {
        dto.Username = dto.Username.Trim().ToLower();
        var exist = await _userRepository.GetByUsernameAsync(dto.Username);

        if (exist is not null)
            throw new AlreadyExistException("User already exist with this Username");

        var newUser = _mapper.Map<User>(dto);
        newUser = await _userRepository.CreateAsync(newUser);

        return _mapper.Map<UserResultDto>(newUser);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            throw new NotFoundException("User not found");

        return await _userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserResultDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserResultDto>>(users);
    }

    public Task<UserResultDto> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResultDto> GetByUsernameAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        return _mapper.Map<UserResultDto>(user);
    }

    public async Task<UserResultDto> ModifyAsync(UserUpdateDto dto)
    {
        dto.Username = dto.Username.Trim().ToLower();

        var exist = await _userRepository.GetByIdAsync(dto.Id);

        if (exist is null)
            throw new NotFoundException("User not found");

        if (exist.Username != dto.Username)
        {
            var existUser = await _userRepository.GetByUsernameAsync(dto.Username);
            if (existUser is not null)
                throw new AlreadyExistException("User already exist with this Username");
        }

        _mapper.Map(dto, exist);

        await _userRepository.UpdateAsync(exist);
        return _mapper.Map<UserResultDto>(exist);
    }

    public async Task<UserResultDto> ModifyPasswordAsync(long id, string oldPass, string newPass)
    {
        var exist = await _userRepository.GetByIdAsync(id);

        if (exist is null)
            throw new NotFoundException("User not found");

        if (oldPass != exist.Password)
            throw new CustomException(403, "Passwor is invalid");

        return _mapper.Map<UserResultDto>(exist);
    }
}
