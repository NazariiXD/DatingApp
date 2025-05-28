using DatingApp.Application.DTOs.Users.Photos;
using DatingApp.Domain.Entities;

namespace DatingApp.Application.Repositories;

public interface IPhotoRepository
{
    Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos();
    Task<Photo?> GetPhotoById(int id);
    void RemovePhoto(Photo photo);
}
