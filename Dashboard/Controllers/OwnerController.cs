using Microsoft.AspNetCore.Mvc;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Infrastructure.Services;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Benaa.Dashboard.Models;
using IncomsInfos = Benaa.Dashboard.Models.IncomsInfos;

using Benaa.Core.Services;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Benaa.Dashboard.Controllers
{
    public class OwnerController : Controller
    {
        private readonly IOwnerService _IOwnerService;
		private readonly IAuthService _IAuthService;
        private readonly IUserService _IUserService;
       
        public OwnerController(IOwnerService OwnerService, IAuthService AuthService, IUserService IUserService)
        {
            _IOwnerService = OwnerService;
			_IAuthService = AuthService;
            _IUserService = IUserService;

        }

		List<JoinPayment> data;
		public async Task<ActionResult> Income(string? type)
        {
            var data1 = await _IOwnerService.getInfo();
            var info = new IncomsInfos
            {
                students = data1.students,
                teachers = data1.teachers,
                dues = data1.dues,
                AllIncoms = data1.AllIncoms,
                Profits = data1.Profits,
            };
			if (type == "P")
			{
				 data = await _IOwnerService.GetPaid();
			}
            else {data = await _IOwnerService.GetDues(); }
		
			return View((info, data));
        }

		public async Task<ActionResult> Get()
		{
			var paid= await _IOwnerService.GetPaid();

			return RedirectToAction("Income", "Owner"); 
		}
		
		public async Task<ActionResult> Done(Guid Id)
        {
			await _IOwnerService.delpay(Id);

			return RedirectToAction("Income", "Owner"); ;
		}
		public async Task<ActionResult> Admins()
        {
            var model =await _IOwnerService.GetA();

			return View(model);
        }

		
		[HttpPost]
        public async Task< ActionResult> Admins(string name,string email,string password)
        {
           
            var AdminRegester = new AdminRegesterDTO
			{
               FirstName= name,
               Email= email,    
               Password= password
            };
            await _IAuthService.RegisterAdmin(AdminRegester);

            return RedirectToAction("Admins");
        }

        public async Task< ActionResult> EditAdmin(string Id)
        {
            var user = await _IUserService.Getuser(Id);
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> EditAdmin(string id, string name, string email,string password)
        {

            var EditAdmin = new UpdateUserInfo
            {
                Id = id,
                FirstName = name,
                Email = email,
                Password= password,
                IsApproved = true,
            };
            await _IOwnerService.UpdateUser(EditAdmin);
            return RedirectToAction("Admins");
        }
       
        public async Task<ActionResult> Del(string Id)
        {
            await _IOwnerService.del(Id);
            
            return RedirectToAction("Admins", "Owner");
            
        }





    }
}
