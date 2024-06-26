﻿using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Numerics;

namespace Benaa.Api.Controllers
{
    [Route("api/moneyCode")]
    [ApiController]
    public class MoneyCodeController : ControllerBase
    {
        private readonly IMoneyCodeService _moneycodeService;

        public MoneyCodeController(IMoneyCodeService moneycodeService)
        {
            _moneycodeService = moneycodeService;
        }

        [HttpPost("GenerateCodes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateCode(int amount, int number)
        {
            try
            {
                if (amount == 0 || number == 0) { return BadRequest("Please input all required data&Cannot be 0"); }
                var result = await _moneycodeService.Generate(amount, number);
                if(result.IsError) { return BadRequest(result.ErrorsOrEmptyList); }
				return Ok(result.Value);
            }
               
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            


        }


        
    }
}
