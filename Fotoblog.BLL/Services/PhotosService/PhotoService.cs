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
using System.IO;
using System.Net;

namespace Fotoblog.BLL.Services.PhotosService
{
    public class PhotoService : BaseService, IPhotoService
    {
        private readonly string[] _allowedExtensions = { ".jpg", "jpeg", ".png" };
        private readonly string[] _allowedImgContentTypes = { "image/jpeg", "image/png" };
        private string? _uploadedFileExtension;
        private string _saveLocation = "c:\\uploads"; // random folder added later
        private readonly int _thumbnailWidth = 600;
        public PhotoService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ServiceResult> AddPhoto(NewPhotoDataVm newPhotoDataVm)
        {
            try
            {
                doInputValidation(newPhotoDataVm);
                PreparePhotoSaveLocation();
            }
            catch(Exception)
            {
                return ServiceResult.Fail(ErrorCodes.CouldNotSaveTheFile);
            }
                

            var origSavePath = Path.Combine(_saveLocation, $"orginal{_uploadedFileExtension}");
            var thumbnailSavePath = Path.Combine(_saveLocation, $"thumbnail{_uploadedFileExtension}");

            try
            {
                await saveOriginalPhoto(origSavePath, newPhotoDataVm.file);
                await saveThumbnail(thumbnailSavePath, newPhotoDataVm.file);
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
            entity.CreateDate = DateTime.Now;
            entity.Tags = _dbContext.TagEntities.Where(t => newPhotoDataVm.tags.Contains(t.Id)).ToList();

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task saveThumbnail(string path, IFormFile file)
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
                    return new JpegEncoder();
                case ".png":
                    return new PngEncoder();
                default:
                    throw new Exception();
            }
        }

        private async Task saveOriginalPhoto(string path, IFormFile file)
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

        private void PreparePhotoSaveLocation()
        {
            do
            {
                _saveLocation = Path.Combine(
                    _saveLocation,
                    Path.GetRandomFileName() );

                if (!Directory.Exists(_saveLocation))
                {
                    Directory.CreateDirectory(_saveLocation);
                    return;
                }

            } while(true);
        }
    }
}
