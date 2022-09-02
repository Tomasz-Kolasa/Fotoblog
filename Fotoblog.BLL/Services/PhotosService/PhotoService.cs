using AutoMapper;
using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.DAL;
using Fotoblog.DAL.Entities;
using Fotoblog.Utils.ViewModels.Photos;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Fotoblog.BLL.Services.PhotosService
{
    public class PhotoService : BaseService, IPhotoService
    {
        private readonly string _saveLocation;
        private readonly List<string> _allowedExtensions;
        private readonly List<string> _allowedImgContentTypes;
        private string? _uploadedFileExtension;
        private readonly int _thumbnailWidth = 600;
        public PhotoService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            var AppConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            _saveLocation = Path.Combine(
                AppConfig.GetSection("AppSettings:PhotosUpload:BaseSavePath")
                    .Value.ToString(),
                Guid.NewGuid().ToString()
                );

            _allowedExtensions = AppConfig.GetSection("AppSettings:PhotosUpload:AllowedExtensions")
                .GetChildren().ToList().Select(x => x.Value).ToList();

            _allowedImgContentTypes = AppConfig.GetSection("AppSettings:PhotosUpload:AllowedImgContentTypes")
                .GetChildren().ToList().Select(x => x.Value).ToList();

            _thumbnailWidth = Int32.Parse(
                AppConfig.GetSection("AppSettings:PhotosUpload:ThumbnailWidth").Value
                );
        }

        public async Task<ServiceResult> AddPhoto(NewPhotoDataVm newPhotoDataVm)
        {
            try
            {
                doInputValidation(newPhotoDataVm);
                Directory.CreateDirectory(_saveLocation);
            }
            catch(Exception)
            {
                return ServiceResult.Fail(ErrorCodes.CouldNotSaveTheFile);
            }
                

            var origSavePath = Path.Combine(_saveLocation, $"orginal{_uploadedFileExtension}");
            var thumbnailSavePath = Path.Combine(_saveLocation, $"thumbnail{_uploadedFileExtension}");

            try
            {
                await SaveOriginalPhoto(origSavePath, newPhotoDataVm.file);
                await SaveThumbnail(thumbnailSavePath, newPhotoDataVm.file);
            }
            catch (Exception)
            {
                return ServiceResult.Fail(ErrorCodes.CouldNotSaveTheFile);
            }

            await SaveEntity(newPhotoDataVm);

            return ServiceResult.Ok();
        }

        private async Task SaveEntity(NewPhotoDataVm newPhotoDataVm)
        {
            var entity = new PhotoEntity();
            entity.Title = newPhotoDataVm.title;
            entity.Description = newPhotoDataVm.description;
            entity.ImagePath = _saveLocation;
            entity.Tags = _dbContext.TagEntities.Where(t => newPhotoDataVm.tags.Contains(t.Id)).ToList();

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SaveThumbnail(string path, IFormFile file)
        {
            using (Image image = Image.Load(file.OpenReadStream()))
            {
                int width = _thumbnailWidth;
                int height = 0; // will automatically determine the correct dimensions size to preserve the original aspect ratio
                image.Mutate(x => x.Resize(width, height, KnownResamplers.Lanczos3));

                using (var outStream = File.Create(path))
                {                 
                    await image.SaveAsync(outStream, GetImageEncoder());
                }
            }
        }

        private IImageEncoder GetImageEncoder()
        {
            switch(_uploadedFileExtension)
            {
                case ".jpg":
                case ".jpeg":
                    return new JpegEncoder();
                case ".png":
                    return new PngEncoder();
                default:
                    throw new Exception();
            }
        }

        private async Task SaveOriginalPhoto(string path, IFormFile file)
        {
            using (var stream = File.Create(path))
            {
                await file.CopyToAsync(stream);
            }
        }

        private void doInputValidation(NewPhotoDataVm newPhotoDataVm)
        {
            if(newPhotoDataVm.file == null)
            {
                throw new ArgumentException();
            }

            _uploadedFileExtension = Path.GetExtension(newPhotoDataVm.file.FileName).ToLowerInvariant();

            if (!isFileExtensionAllowed() || !isFileContentTypeAllowed(newPhotoDataVm.file.ContentType))
            {
                throw new ArgumentException();
            }

            if (newPhotoDataVm.file.Length == 0)
            {
                throw new ArgumentException();
            }

            if( string.IsNullOrEmpty(newPhotoDataVm.title) ||
                newPhotoDataVm.title.Length < 2 || newPhotoDataVm.title.Length > 15 )
            {
                throw new ArgumentException();
            }
            else
            {
                newPhotoDataVm.title =  WebUtility.HtmlEncode(newPhotoDataVm.title);
            }

            if (!string.IsNullOrEmpty(newPhotoDataVm.description) )
            {
                if(newPhotoDataVm.description.Length > 30)
                {
                    throw new ArgumentException();
                }
                else
                {
                    newPhotoDataVm.description = WebUtility.HtmlEncode(newPhotoDataVm.description);
                }
            }
        }

        private bool isFileExtensionAllowed()
        {
            
            return (
                string.IsNullOrEmpty(_uploadedFileExtension)
                ||!_allowedExtensions.Contains(_uploadedFileExtension)
                ) ? false : true;
        }

        private bool isFileContentTypeAllowed(string contentType)
        {

            return (
                string.IsNullOrEmpty(contentType)
                || !_allowedImgContentTypes.Contains(contentType)
                ) ? false : true;
        }
    }
}
