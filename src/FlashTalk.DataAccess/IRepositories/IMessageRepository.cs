using FlashTalk.Domain.Entities;

namespace FlashTalk.DataAccess.IRepositories;

public interface IMessageRepository
{
    Task<bool> CreateAsync(Message message);
    Task<bool> UpdateAsync(Message message);
    Task<Message> GetByIdAsync(long messageId);
    Task<bool> DeleteAsync(long messageId);
    Task<IEnumerable<Message>> GetAllMessagesForUserAsync(long userId);
    Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(long user1Id, long user2Id);
}