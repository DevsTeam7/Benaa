﻿using Benaa.Core.Entities.General;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TestController : ControllerBase
    {

        [Authorize]
        [HttpGet("Index")]
        public string Index()
        {
            return "done";
        }
    }
}
