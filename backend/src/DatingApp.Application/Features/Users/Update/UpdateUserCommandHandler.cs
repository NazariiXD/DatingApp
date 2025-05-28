using AutoMapper;
using DatingApp.Application.Repositories;
using DatingApp.Domain.Exceptions;
using MediatR;

namespace DatingApp.Application.Features.Users.Update;

public class UpdateUserCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(request.Username);

        if (user == null)
        {
            throw new NotFoundException("Could not find user");
        }

        mapper.Map(request.MemberUpdate, user);

        if (!await unitOfWork.Complete())
        {
            throw new InvalidOperationException("Failed to update user");
        }

        return Unit.Value;
    }
}