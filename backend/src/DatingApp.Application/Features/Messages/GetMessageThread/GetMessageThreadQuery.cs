using DatingApp.Application.DTOs.Messages;
using MediatR;

namespace DatingApp.Application.Features.Messages.GetMessageThread;

public record GetMessageThreadQuery(string Username, string CurrentUsername)
    : IRequest<IEnumerable<MessageDto>>;