using DatingApp.Application.DTOs;
using DatingApp.Application.DTOs.Likes;
using DatingApp.Application.DTOs.Users;
using MediatR;

namespace DatingApp.Application.Features.Likes.GetUserLikes;

public record GetUserLikesQuery(LikesParams LikesParams)
    : IRequest<PagedList<MemberDto>>;