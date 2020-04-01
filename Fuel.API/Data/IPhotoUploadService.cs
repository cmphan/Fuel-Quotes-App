using CloudinaryDotNet;

namespace Fuel.API.Data
{
    public interface IPhotoUploadService
    {
        Cloudinary GetCloudinaryService();
    }
}