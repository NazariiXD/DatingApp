using DatingApp.Application.DTOs;
using DatingApp.Application.DTOs.Likes;
using DatingApp.Application.DTOs.Users;
using DatingApp.Domain.Entities;

namespace DatingApp.Application.Repositories;

public interface ILikesRepository
{
    Task<UserLike?> GetUserLike(int sourceUserId, int targetUserId);
    Task<PagedList<MemberDto>> GetUserLikes(LikesParams likesParams);
    Task<IEnumerable<int>> GetCurrentUserLikeIds(int currentUserId);
    void DeleteLike(UserLike like);
    void AddLike(UserLike like);
}
