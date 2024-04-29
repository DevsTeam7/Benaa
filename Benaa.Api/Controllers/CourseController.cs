using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Benaa.Core.Entities.General;
using Microsoft.AspNetCore.Authorization;
using Benaa.Infrastructure.Utils.Users;

namespace Benaa.Api.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userManager;
        public CourseController(ICourseService courseService, UserManager<User> userManager) {
            _courseService = courseService;
            _userManager = userManager;
        }
        [Authorize(Roles = Role.Teacher)]
        [HttpPost("Create"), RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue, ValueLengthLimit = Int32.MaxValue),DisableRequestSizeLimit]
        public async Task<IActionResult> Create([FromForm] CreateCourseDto courseDto)
        {
           var TeacherId = _userManager.GetUserId(HttpContext.User);
           var res = await _courseService.CreateCourseOnly(courseDto, TeacherId!);
            if (res.IsError) { return BadRequest(res.ErrorsOrEmptyList); }
            return Created("",res.Value);
        }
        [HttpPost("CreateChLe"), RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue, ValueLengthLimit = Int32.MaxValue), DisableRequestSizeLimit]
        public async Task<IActionResult> CreaCreateChLete([FromForm] ChapterLessonsDto chapterLessons)
        {
            var TeacherId = _userManager.GetUserId(HttpContext.User);
            Guid id = Guid.Parse("9269a443-41cb-4f2c-9410-b19d1ea6fe11");
            var res = await _courseService.CreateChapterLessons(chapterLessons, id);
            if (res.IsError) { return BadRequest(res.ErrorsOrEmptyList); }
            return Created("", res.Value);
        }


    }
}
