using DatingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Features.Admin.GetUsersWithRoles;

public class GetUsersWithRolesQueryHandler(
    UserManager<AppUser> userManager)
    : IRequestHandler<GetUsersWithRolesQuery, IEnumerable<object>>
{
    public async Task<IEnumerable<object>> Handle(GetUsersWithRolesQuery request, CancellationToken cancellationToken)
    {
        return await userManager.Users
            .OrderBy(x => x.UserName)
            .Select(x => new
            {
                x.Id,
                Username = x.UserName,
                Roles = x.UserRoles.Select(r => r.Role.Name).ToList()
            }).ToListAsync(cancellationToken);
    }
}