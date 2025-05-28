using DatingApp.Application.DTOs;
using DatingApp.Application.DTOs.Messages;
using DatingApp.Application.Repositories;
using MediatR;

namespace DatingApp.Application.Features.Messages.GetMessagesForUser;

public class GetMessagesForUserQueryHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<GetMessagesForUserQuery, PagedList<MessageDto>>
{
    public async Task<PagedList<MessageDto>> Handle(GetMessagesForUserQuery request, CancellationToken cancellationToken)
    {
        var messages = await unitOfWork.MessageRepository.GetMessagesForUser(request.MessageParams);
        return messages;
    }
}