using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTube.BussinessManagers.Interfaces;
using YouTube.Data.Models;
using YouTube.Models.VideoViewModels;

namespace YouTube.Controllers
{
    [Authorize]
    public class VideoController : Controller
    {
        private readonly IVideoBussinessManager videoBussinessManager;

        public VideoController(IVideoBussinessManager videoBussinessManager)
        {
            this.videoBussinessManager = videoBussinessManager;
        }

        [Route("Video/{id}"), AllowAnonymous]
        public async Task<IActionResult> Index(int? id)
        {
            var actionResult = await videoBussinessManager.GetVideoViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        public IActionResult Create()
        {
            return View(new CreateViewModel()
            {
                Video = new Video()
            });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var actionResult = await videoBussinessManager.GetEditViewModel(id, User);
            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }


        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Add(CreateViewModel model)
        {
            await videoBussinessManager.CreateVideo(model, User);
            return RedirectToAction("Create");
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            var actionResult = await videoBussinessManager.UpdateVideo(editViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Edit", new { editViewModel.Video.Id });

            return actionResult.Result;

        }
    }
}
