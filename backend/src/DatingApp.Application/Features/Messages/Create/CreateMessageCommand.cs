using DatingApp.Application.DTOs.Messages;
using MediatR;

namespace DatingApp.Application.Features.Messages.Create;

public record CreateMessageCommand(CreateMessageDto CreateMessage, string Username)
    : IRequest<MessageDto>;