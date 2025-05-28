using AutoMapper;
using DatingApp.Application.DTOs.Messages;
using DatingApp.Application.Repositories;
using DatingApp.Domain.Entities;
using MediatR;

namespace DatingApp.Application.Features.Messages.Create;

public class CreateMessageCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<CreateMessageCommand, MessageDto>
{
    public async Task<MessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var createMessageDto = request.CreateMessage;
        var username = request.Username.ToLower();

        if (username == createMessageDto.RecipientUsername.ToLower())
        {
            throw new ArgumentException("You cannot message yourself");
        }

        var sender = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);
        var recipient = await unitOfWork.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

        if (recipient == null || sender == null || sender.UserName == null || recipient.UserName == null)
        {
            throw new ArgumentException("Cannot send message at this time");
        }

        var message = new Message
        {
            Sender = sender,
            Recipient = recipient,
            SenderUsername = sender.UserName,
            RecipientUsername = recipient.UserName,
            Content = createMessageDto.Content
        };

        unitOfWork.MessageRepository.AddMessage(message);

        if (!await unitOfWork.Complete())
        {
            throw new InvalidOperationException("Failed to save message");
        }

        return mapper.Map<MessageDto>(message);
    }
}