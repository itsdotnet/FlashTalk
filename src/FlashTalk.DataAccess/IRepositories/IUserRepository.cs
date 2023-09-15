using FlashTalk.Domain.Entities;

namespace FlashTalk.DataAccess.IRepositories;

public interface IUserRepository
{
    Task<User> GetAllAsync();
    Task<User> DeleteAsync(long id);
    Task<User> GetByIdAsync(long id);
    Task<User> UpdateAsync(User user);
    Task<User> CreateAsync(User user);
    Task<User> GetByUsernameAsync(string username);
}
