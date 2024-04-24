using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Benaa.Core.Entities.General;

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
        [HttpPost("Create")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create([FromForm] CreateCourseDto courseDto)
        {
           var id = _userManager.GetUserId(HttpContext.User);
            courseDto.TeacherId = id;
           var res = await _courseService.CreateCourseOnly(courseDto,id);
            if (res.IsError) { return BadRequest(res.ErrorsOrEmptyList); }
            return Created("",res.Value);
        }

    }
}
