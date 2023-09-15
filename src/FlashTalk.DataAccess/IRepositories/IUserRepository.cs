using FlashTalk.Domain.Entities;

namespace FlashTalk.DataAccess.IRepositories;

public interface IUserRepository
{
    Task<bool> DeleteAsync(long id);
    Task<User> GetByIdAsync(long id);
    Task<User> UpdateAsync(User user);
    Task<User> CreateAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByUsernameAsync(string username);
}
