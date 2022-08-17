using AutoMapper;
using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.DAL;
using Fotoblog.Utils.ViewModels.Photos;

namespace Fotoblog.BLL.Services.PhotosService
{
    public class PhotoService : BaseService, IPhotoService
    {
        private readonly string[] _allowedExtensions = { ".jpg", "jpeg", ".png" };
        public PhotoService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ServiceResult> AddPhoto(NewPhotoDataVm newPhotoDataVm)
        {
            var uploadedFileNameExtension = Path.GetExtension(newPhotoDataVm.file.FileName).ToLowerInvariant();

            if ( !isFileExtensionAllowed(uploadedFileNameExtension) )
            {
                return ServiceResult.Fail(ErrorCodes.FileExtensionNotAllowed);
            }

            if(newPhotoDataVm.file.Length == 0)
            {
                return ServiceResult.Fail(ErrorCodes.FileLengthZero);
            }

            var saveLocation = "";

            try
            {
                saveLocation = GetSaveLocation();
            }
            catch(Exception e)
            {
                return ServiceResult.Fail(ErrorCodes.CouldNotSaveTheFile);
            }
                

            var imgPathOrig = Path.Combine(saveLocation, $"orginal{uploadedFileNameExtension}");

            try
            {
                using (var stream = File.Create(imgPathOrig))
                {
                    await newPhotoDataVm.file.CopyToAsync(stream);
                }
            } catch (Exception e)
            {
                var x = e;
            }


            return ServiceResult.Ok();
        }

        private bool isFileExtensionAllowed(string ext)
        {
            
            return (string.IsNullOrEmpty(ext) || !_allowedExtensions.Contains(ext)) ? false : true;
        }

        private string GetSaveLocation()
        {
            do
            {
                var locationPath = Path.Combine(
                    // _config["StoredFilesPath"],
                    "c:\\uploads",
                    Path.GetRandomFileName() );

                if (!Directory.Exists(locationPath))
                {
                    Directory.CreateDirectory(locationPath);

                    return locationPath;
                }

            } while(true);
        }
    }
}
