using Fotoblog.BLL.Services.TagService;
using Fotoblog.Utils.ViewModels.Tags;
using Microsoft.AspNetCore.Mvc;

namespace Fotoblog.Controllers
{
    public class TagsController : BaseController
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="createTagVm">VM containing tag name</param>
        /// <returns>Task result</returns>
        [HttpPost]
        public async Task<IActionResult> AddNew([FromBody] NewTagVm createTagVm )
        {
            var result = await _tagService.AddNewTag(createTagVm);
            return FromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tagService.GetAllTags();
            return FromResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTagVm updateTag)
        {
            var result = await _tagService.UpdateTag(updateTag);
            return FromResult(result);
        }

        [HttpDelete("{tagId}")]
        public async Task<IActionResult> Remove(int tagId)
        {
            var result = await _tagService.RemoveTag(tagId);
            return FromResult(result);
        }
    }
}
