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
        public async Task<IActionResult> Create([FromForm] CreateCourseDto courseDto){
            if(ModelState.IsValid){
                try {
                    var TeacherId = _userManager.GetUserId(HttpContext.User);
                    var result = await _courseService.CreateCourseOnly(courseDto, TeacherId!);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Created("", result.Value);
                }
                catch (Exception ex){
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required filds");
        }

        [Authorize(Roles = Role.Teacher)]
        [HttpPost("CreateChapterLessons"), RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue, ValueLengthLimit = Int32.MaxValue), DisableRequestSizeLimit]
        public async Task<IActionResult> CreateChapterLessons([FromForm] ChapterLessonsDto chapterLessons, string courseId){
            if (ModelState.IsValid){
                try{
                    var result = await _courseService.CreateChapterLessons(chapterLessons, courseId);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Created("", result.Value);
                }
                catch (Exception ex){
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required filds");
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(string courseId){
            if(string.IsNullOrEmpty(courseId)){return BadRequest();}
            try
            {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var cartId = await _courseService.AddCourseToCart(courseId, studentId);
                if (cartId.IsError) { return BadRequest(cartId.ErrorsOrEmptyList); }
                return Ok(cartId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
         
        [HttpPost("AddRate")]
        public async Task<IActionResult> AddRate(RateDTO.Request newRate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var TeacherId = _userManager.GetUserId(HttpContext.User);
                    var result = await _courseService.AddRate(newRate, TeacherId);
                    if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                    return Created("", result.Value);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("Please input all required filds");
        }

        [HttpGet("GetAllRate")]
        public async Task<IActionResult> GetAllRate(string courseId)
        {
            if (string.IsNullOrEmpty(courseId)) { return BadRequest(); }
            try{
                var rates = await _courseService.GetAllRate(courseId);
                if (rates.IsError) { return BadRequest(rates.ErrorsOrEmptyList); }
                return Ok(rates);
            }
            catch (Exception ex){
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Buy")]
        public async Task<IActionResult> Buy(string courseId)
        {
            if (string.IsNullOrEmpty(courseId)) { return BadRequest(); }
            try
            {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var course = await _courseService.BuyCourse(studentId, courseId);
                if (course.IsError) { return BadRequest(course.ErrorsOrEmptyList); }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string courseId)
        {
            try
            {
               await _courseService.Delete(courseId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string courseId)
        {
            if (string.IsNullOrEmpty(courseId)) { return BadRequest(); }
            try
            {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var course = await _courseService.GetById(courseId);
                if (course.IsError) { return BadRequest(course.ErrorsOrEmptyList); }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetChapterLessons")]
        public async Task<IActionResult> GetChapterLessons(string courseId)
        {
            if (string.IsNullOrEmpty(courseId)) { return BadRequest(); }
            try
            {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var course = await _courseService.GetChapterLessons(courseId);
                if (course.IsError) { return BadRequest(course.ErrorsOrEmptyList); }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetByStudentId")]
        public async Task<IActionResult> GetByStudentId()
        {
            try
            {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var courses = await _courseService.GetByStudentId(studentId);
                if (courses.IsError) { return BadRequest(courses.ErrorsOrEmptyList); }
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetByTeacherId")]
        public async Task<IActionResult> GetByTeacherId()
        {
            try
            {
                var teacherId = _userManager.GetUserId(HttpContext.User);
                var courses = await _courseService.GetByTeacherId(teacherId);
                if (courses.IsError) { return BadRequest(courses.ErrorsOrEmptyList); }
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetByType")]
        public async Task<IActionResult> GetByType(int courseType)
        {
            try
            {
                var teacherId = _userManager.GetUserId(HttpContext.User);
                var courses = await _courseService.GetByType(courseType);
                if (courses.IsError) { return BadRequest(courses.ErrorsOrEmptyList); }
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
