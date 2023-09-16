using FlashTalk.Domain.Entities;
using FlashTalk.Service.DTOs.Messages;

namespace FlashTalk.Service.Interfaces;

public interface IMessageService
{
    Task<bool> DeleteMessageAsync(long messageId);
    Task<Message> GetMessageByIdAsync(long messageId);
    Task<bool> UpdateMessageAsync(MessageUpdateDto message);
    Task<Message> SendMessageAsync(MessageCreationDto message);
    Task<IEnumerable<Message>> GetMessagesForUserAsync(long userId);
    Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(long user1Id, long user2Id);
}
