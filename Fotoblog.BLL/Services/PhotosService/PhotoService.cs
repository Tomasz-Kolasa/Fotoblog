using AutoMapper;
using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.DAL;
using Fotoblog.Utils.ViewModels.Photos;
using System.Net;
using System.Text.Encodings.Web;

namespace Fotoblog.BLL.Services.PhotosService
{
    public class PhotoService : BaseService, IPhotoService
    {
        private readonly string[] _allowedExtensions = { ".jpg", "jpeg", ".png" };
        private readonly string[] _allowedImgContentTypes = { "image/jpeg", "image/png" };
        private string? _uploadedFileNameExtension;
        private string _saveLocation;
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
                

            var imgPathOrig = Path.Combine(_saveLocation, $"orginal{_uploadedFileNameExtension}");

            using (var stream = File.Create(imgPathOrig))
            {
                await newPhotoDataVm.file.CopyToAsync(stream);
            }

            return ServiceResult.Ok();
        }

        private void doInputValidation(NewPhotoDataVm newPhotoDataVm)
        {
            if(newPhotoDataVm.file == null)
            {
                throw new ArgumentException();
            }

            _uploadedFileNameExtension = Path.GetExtension(newPhotoDataVm.file.FileName).ToLowerInvariant();

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
                string.IsNullOrEmpty(_uploadedFileNameExtension)
                ||!_allowedExtensions.Contains(_uploadedFileNameExtension)
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
                    // _config["StoredFilesPath"],
                    "c:\\uploads",
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
