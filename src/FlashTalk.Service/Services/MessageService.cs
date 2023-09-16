using AutoMapper;
using FlashTalk.DataAccess.IRepositories;
using FlashTalk.Domain.Entities;
using FlashTalk.Service.DTOs.Messages;
using FlashTalk.Service.Exceptions;
using FlashTalk.Service.Interfaces;

namespace FlashTalk.Service.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public MessageService(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<Message> SendMessageAsync(MessageCreationDto messageDto)
    {
        var message = _mapper.Map<Message>(messageDto);

        await _messageRepository.CreateAsync(message);
        return message;
    }

    public async Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(long user1Id, long user2Id)
        => await _messageRepository.GetMessagesBetweenUsersAsync(user1Id, user2Id);

    public async Task<IEnumerable<Message>> GetMessagesForUserAsync(long userId)
        => await _messageRepository.GetAllMessagesForUserAsync(userId);

    public async Task<Message> GetMessageByIdAsync(long messageId)
        => await _messageRepository.GetByIdAsync(messageId);

    public async Task<bool> UpdateMessageAsync(MessageUpdateDto messageDto)
    {
        var existingMessage = await _messageRepository.GetByIdAsync(messageDto.Id);

        if (existingMessage is null)
            throw new NotFoundException("Message not found");

        existingMessage.Text = messageDto.Text;
        return await _messageRepository.UpdateAsync(existingMessage);
    }

    public async Task<bool> DeleteMessageAsync(long messageId)
    {
        return await _messageRepository.DeleteAsync(messageId);
    }
}
