using Fotoblog.BLL.Services.TagService;
using Fotoblog.Controllers.ActionFilter;
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
        [ValidateModel]
        public async Task<IActionResult> AddNewTag([FromBody] CreateTagVm createTagVm )
        {
            var result = await _tagService.AddNewTag(createTagVm);
            return FromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllTags()
        {
            var result = await _tagService.GetAllTags();
            return FromResult(result);
        }
    }
}
