using DatingApp.Application.DTOs;
using DatingApp.Application.DTOs.Messages;
using MediatR;

namespace DatingApp.Application.Features.Messages.GetMessagesForUser;

public record GetMessagesForUserQuery(MessageParams MessageParams, string Username)
    : IRequest<PagedList<MessageDto>>;