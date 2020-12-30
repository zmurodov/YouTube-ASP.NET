using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTube.BussinessManagers.Interfaces;
using YouTube.Models.AdminViewModels;

namespace YouTube.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminBussinessManager adminBussinessManager;

        public AdminController(IAdminBussinessManager adminBussinessManager)
        {
            this.adminBussinessManager = adminBussinessManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await adminBussinessManager.GetAdminDashboard(User));
        }

        public async Task<IActionResult> About()
        {
            return View(await adminBussinessManager.GetAboutViewModel(User));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(AboutViewModel aboutViewModel)
        {
            await adminBussinessManager.UpdateAbout(aboutViewModel, User);
            return RedirectToAction("About");
        }
    }
}
