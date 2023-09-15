using FlashTalk.Service.DTOs.Users;

public interface IUserService
{
    Task<bool> DeleteAsync(long id);
    Task<UserResultDto> GetByIdAsync(long id);
    Task<IEnumerable<UserResultDto>> GetAllAsync();
    Task<UserResultDto> ModifyAsync(UserUpdateDto dto);
    Task<UserResultDto> CreateAsync(UserCreationDto dto);
    Task<UserResultDto> GetByUsernameAsync(string username);
    Task<bool> CheckUserAsync(string username, string password);
    Task<UserResultDto> ModifyPasswordAsync(long id, string oldPass, string newPass);
}