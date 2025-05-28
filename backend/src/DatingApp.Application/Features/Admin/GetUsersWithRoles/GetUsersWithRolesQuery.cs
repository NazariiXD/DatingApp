using MediatR;

namespace DatingApp.Application.Features.Admin.GetUsersWithRoles;

public record GetUsersWithRolesQuery()
    : IRequest<IEnumerable<object>>;