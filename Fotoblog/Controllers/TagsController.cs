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

        [HttpPost]
        public async Task<IActionResult> AddNewTag([FromBody] NewTagVm createTagVm )
        {
            var result = await _tagService.AddNewTag(createTagVm);
            return FromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var result = await _tagService.GetAllTags();
            return FromResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTag([FromBody] UpdateTagVm updateTag)
        {
            var result = await _tagService.UpdateTag(updateTag);
            return FromResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTag([FromBody] RemoveTagVm removeTagVm)
        {
            var result = await _tagService.RemoveTag(removeTagVm);
            return FromResult(result);
        }
    }
}
