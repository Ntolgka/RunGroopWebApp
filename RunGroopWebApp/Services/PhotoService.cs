using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.Interfaces;

namespace RunGroopWebApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(account);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

            if (file.Length > 0 && allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                return await _cloudinary.UploadAsync(uploadParams);
            }
            else
            {
                throw new Exception("Unsupported image file. Only JPG, JPEG, and PNG formats are allowed.");
            }
        }

        public Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
