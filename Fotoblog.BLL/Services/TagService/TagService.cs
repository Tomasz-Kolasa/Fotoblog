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

        public async Task<ServiceResult> AddNewTag(NewTagVm createTagVm)
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
            var tags = _mapper.Map<List<TagVm>>(readTags);

            return ServiceResult<List<TagVm>>.Ok(tags);            
        }

        public async Task<ServiceResult> UpdateTag(UpdateTagVm updateTagVm)
        {
            var requestedTag = await _dbContext.TagEntities.FirstOrDefaultAsync(x => x.Id == updateTagVm.Id);

            if(requestedTag == null)
            {
                return ServiceResult.Fail(ErrorCodes.TagNotExists);
            }

            requestedTag.Name = updateTagVm.NewName;
            await _dbContext.SaveChangesAsync();
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> RemoveTag(RemoveTagVm removeTagVm)
        {
            var tagToRemove = await _dbContext.TagEntities.FirstOrDefaultAsync(x => x.Id == removeTagVm.Id);

            if (tagToRemove == null)
            {
                return ServiceResult.Fail(ErrorCodes.TagNotExists);
            }

            _dbContext.TagEntities.Remove(tagToRemove);
            await _dbContext.SaveChangesAsync();
            return ServiceResult.Ok();
        }
    }
}
