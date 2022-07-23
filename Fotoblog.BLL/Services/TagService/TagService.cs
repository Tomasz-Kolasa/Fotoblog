using AutoMapper;
using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.DAL;
using Fotoblog.DAL.Entities;
using Fotoblog.Utils.ViewModels.Tags;
using Microsoft.EntityFrameworkCore;

namespace Fotoblog.BLL.Services.TagService
{
    public class TagService : BaseService, ITagService
    {
        public TagService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ServiceResult> AddNewTag(CreateTagVm createTagVm)
        {
            var isTagAlreadyExists = await _dbContext.TagEntities.AnyAsync(x => x.Name == createTagVm.Name);

            if(isTagAlreadyExists)
            {
                return ServiceResult.Fail(ErrorCodes.TagAlreadyCreated);
            }

            var newTagEntity = _mapper.Map<TagEntity>(createTagVm);

            await _dbContext.TagEntities.AddAsync(newTagEntity);
            await _dbContext.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult<List<TagVm>>> GetAllTags()
        {
            var readTags = await _dbContext.TagEntities.ToListAsync();

            List<TagVm> tags = new List<TagVm>();


            if(readTags.Count > 0)
            {
                foreach (var readTag in readTags)
                {
                    tags.Add(_mapper.Map<TagVm>(readTag));
                }

                return ServiceResult<List<TagVm>>.Ok(tags);
            }
            else
            {
                return ServiceResult<List<TagVm>>.Fail(ErrorCodes.NoTagsFound);
            }
            
        }
    }
}
