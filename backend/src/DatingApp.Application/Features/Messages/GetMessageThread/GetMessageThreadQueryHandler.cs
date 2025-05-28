using DatingApp.Application.DTOs.Messages;
using DatingApp.Application.Repositories;
using MediatR;

namespace DatingApp.Application.Features.Messages.GetMessageThread;

public class GetMessageThreadQueryHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<GetMessageThreadQuery, IEnumerable<MessageDto>>
{
    public async Task<IEnumerable<MessageDto>> Handle(GetMessageThreadQuery request, CancellationToken cancellationToken)
    {
        var messages = await unitOfWork.MessageRepository.GetMessageThread(request.CurrentUsername, request.Username);
        return messages;
    }
}