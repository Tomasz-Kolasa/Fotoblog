using AutoMapper;
using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.DAL;
using Fotoblog.Utils.ViewModels.Photos;
using Microsoft.AspNetCore.Http;

namespace Fotoblog.BLL.Services.PhotosService
{
    public class PhotoService : BaseService, IPhotoService
    {
        public PhotoService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ServiceResult> AddPhoto(NewPhotoDataVm newPhotoDataVm)
        {

            await _dbContext.SaveChangesAsync();

            return ServiceResult.Ok();
        }
    }
}
