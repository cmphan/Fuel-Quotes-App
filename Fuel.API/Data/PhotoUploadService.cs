using CloudinaryDotNet;
using Fuel.API.Helpers;
using Microsoft.Extensions.Options;

namespace Fuel.API.Data
{
    public class PhotoUploadService: IPhotoUploadService
    {
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        public PhotoUploadService( IOptions<CloudinarySettings> cloudinaryConfig){
            _cloudinaryConfig = cloudinaryConfig;
        }
        public Cloudinary GetCloudinaryService()
        {
            
            Account acc = new Account (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
            return new Cloudinary(acc);
        }
    }
}