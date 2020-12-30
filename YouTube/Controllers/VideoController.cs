using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTube.BussinessManagers.Interfaces;
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
            return View(new CreateViewModel());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var actionResult = await videoBussinessManager.GetEditViewModel(id, User);
            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }


        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createViewModel)
        {
            await videoBussinessManager.CreateVideo(createViewModel, User);
            return RedirectToAction("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            var actionResult = await videoBussinessManager.UpdateVideo(editViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Edit", new { editViewModel.Video.Id });

            return actionResult.Result;

        }
    }
}
