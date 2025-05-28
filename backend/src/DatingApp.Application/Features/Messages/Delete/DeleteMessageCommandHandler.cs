using DatingApp.Application.Repositories;
using DatingApp.Domain.Exceptions;
using MediatR;

namespace DatingApp.Application.Features.Messages.Delete;

public class DeleteMessageCommandHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteMessageCommand, Unit>
{
    public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var username = request.Username;

        var message = await unitOfWork.MessageRepository.GetMessage(id);

        if (message == null)
        {
            throw new NotFoundException("Cannot delete this message");
        }

        if (message.SenderUsername != username && message.RecipientUsername != username)
        {
            throw new ForbiddenException("Cannot delete this message");
        }

        if (message.SenderUsername == username) message.SenderDeleted = true;
        if (message.RecipientUsername == username) message.RecipientDeleted = true;

        if (message is { SenderDeleted: true, RecipientDeleted: true })
        {
            unitOfWork.MessageRepository.DeleteMessage(message);
        }

        if (!await unitOfWork.Complete())
        {
            throw new InvalidOperationException("Problem deleting the message");
        }

        return Unit.Value;
    }
}