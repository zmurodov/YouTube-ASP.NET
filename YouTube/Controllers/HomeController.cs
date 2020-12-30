using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YouTube.BussinessManagers.Interfaces;
using YouTube.Models;

namespace YouTube.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVideoBussinessManager videoBussinessManager;
        private readonly IHomeBussinessManager homeBussinessManager;

        public HomeController(IVideoBussinessManager videoBussinessManager, IHomeBussinessManager homeBussinessManager)
        {
            this.videoBussinessManager = videoBussinessManager;
            this.homeBussinessManager = homeBussinessManager;
        }

        public IActionResult Index(string searchString, int? page)
        {
            return View(videoBussinessManager.GetIndexViewModel(searchString, page));
        }

        public IActionResult Author(string authorId, string searchString, int? page)
        {
            var actionResult = homeBussinessManager.GetAuthorViewModel(authorId, searchString, page);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }
    }
}
