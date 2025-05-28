using MediatR;

namespace DatingApp.Application.Features.Messages.Delete;

public record DeleteMessageCommand(int Id, string Username)
    : IRequest<Unit>;