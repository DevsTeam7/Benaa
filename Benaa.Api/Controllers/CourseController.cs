﻿using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Benaa.Core.Entities.General;
using Microsoft.AspNetCore.Authorization;
using Benaa.Infrastructure.Utils.Users;
using Microsoft.IdentityModel.Tokens;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddToCart(string courseId){
            if(string.IsNullOrEmpty(courseId)){return BadRequest();}
            try
            {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var cartId = await _courseService.AddCourseToCart(courseId, studentId);
                if (cartId.IsError) { return BadRequest(cartId.ErrorsOrEmptyList); }
                return Ok(cartId.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
         
        [HttpPost("AddRate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRate(string courseId)
        {
            if (string.IsNullOrEmpty(courseId)) { return BadRequest(); }
            try{
                var rates = await _courseService.GetAllRate(courseId);
                if (rates.IsError) { return BadRequest(rates.ErrorsOrEmptyList); }
                return Ok(rates.Value);
            }
            catch (Exception ex){
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Buy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Buy(List<string> courses)
        {
            if (courses.Count == 0) { return BadRequest(); }
            try
            {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var course = await _courseService.BuyCourse(studentId, courses);
                if (course.IsError) { return BadRequest(course.ErrorsOrEmptyList); }
                return Ok(course.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string courseId)
        {
            try
            {
               await _courseService.Delete(courseId);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string courseId)
        {
            if (string.IsNullOrEmpty(courseId)) { return BadRequest(); }
            try
            {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var course = await _courseService.GetById(courseId);
                if (course.IsError) { return BadRequest(course.ErrorsOrEmptyList); }
                return Ok(course.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetChapterLessons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetChapterLessons(string courseId)
        {
            if (string.IsNullOrEmpty(courseId)) { return BadRequest(); }
            try
            {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var course = await _courseService.GetChapterLessons(courseId);
                if (course.IsError) { return BadRequest(course.ErrorsOrEmptyList); }
                return Ok(course.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetByStudentId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStudentId()
        {
            try
            {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var courses = await _courseService.GetByStudentId(studentId);
                if (courses.IsError) { return BadRequest(courses.ErrorsOrEmptyList); }
                return Ok(courses.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetByTeacherId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByTeacherId()
        {
            try
            {
                var teacherId = _userManager.GetUserId(HttpContext.User);
                var courses = await _courseService.GetByTeacherId(teacherId);
                if (courses.IsError) { return BadRequest(courses.ErrorsOrEmptyList); }
                return Ok(courses.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetByType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByType(int courseType)
        {
            try
            {
                var courses = await _courseService.GetByType(courseType);
                if (courses.IsError) { return BadRequest(courses.ErrorsOrEmptyList); }
                return Ok(courses.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpGet("GetByQuantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByQuantity(int quantity, int courseType)
        {
            try
            {
                var courses = await _courseService.GetByQuantity(quantity, courseType);
                if (courses.IsError) { return BadRequest(courses.ErrorsOrEmptyList); }
                return Ok(courses.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost ("ReturnTheCourse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReturnTheCourse(string courseId)
        {
            if(string.IsNullOrWhiteSpace(courseId)) { return BadRequest(); }
            try {
                var studentId = _userManager.GetUserId(HttpContext.User);
                var result = await _courseService.ReturnTheCourse(courseId, studentId!);
                if (result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
