using DatingApp.Domain.Entities;
using DatingApp.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Application.Features.Admin.EditUserRoles;

public class EditUserRolesCommandHandler(
    UserManager<AppUser> userManager)
    : IRequestHandler<EditUserRolesCommand, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(EditUserRolesCommand request, CancellationToken cancellationToken)
    {
        var username = request.Username;
        var roles = request.Roles;

        if (string.IsNullOrEmpty(roles))
        {
            throw new ArgumentException("You must select at least one role");
        }

        var selectedRoles = roles.Split(",").ToArray();

        var user = await userManager.FindByNameAsync(username);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var userRoles = await userManager.GetRolesAsync(user);

        var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to add to roles");
        }

        result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to remove from roles");
        }

        return await userManager.GetRolesAsync(user);
    }
}