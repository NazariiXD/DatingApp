using MediatR;

namespace DatingApp.Application.Features.Admin.EditUserRoles;

public record EditUserRolesCommand(string Username, string Roles)
    : IRequest<IEnumerable<string>>;