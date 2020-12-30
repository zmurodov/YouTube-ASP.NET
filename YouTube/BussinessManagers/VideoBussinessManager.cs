using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YouTube.Authorization;
using YouTube.BussinessManagers.Interfaces;
using YouTube.Data.Models;
using YouTube.Models.HomeViewModel;
using YouTube.Models.VideoViewModels;
using YouTube.Service.Interfaces;

namespace YouTube.BussinessManagers
{
    public class VideoBussinessManager: IVideoBussinessManager
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IVideoService videoService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAuthorizationService authorizationService;

        public VideoBussinessManager(UserManager<ApplicationUser> userManager,
            IVideoService videoService, IWebHostEnvironment webHostEnvironment, IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.videoService = videoService;
            this.webHostEnvironment = webHostEnvironment;
            this.authorizationService = authorizationService;
        }

        public IndexViewModel GetIndexViewModel(string searchString, int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;
            var videos = videoService.GetVideos(searchString ?? string.Empty)
                .Where(video => video.Published);

            return new IndexViewModel
            {
                Videos = new StaticPagedList<Video>(videos.Skip((pageNumber - 1) * pageSize).Take(pageSize),
                            pageNumber, pageSize, videos.Count()),
                SearchString = searchString,
                PageNumber = pageNumber
            };
        }

        public async Task<ActionResult<VideoViewModel>> GetVideoViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var videoId = id.Value;
            var video = videoService.GetVideo(videoId);

            if (video is null)
                return new NotFoundResult();

            if (!video.Published)
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal,
                        video, Operations.Read);
                if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);
            }

            return new VideoViewModel
            {
                Video = video,
            };
        }
        public async Task<Video> CreateVideo(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Video video = createViewModel.Video;

            video.Creator = await userManager.GetUserAsync(claimsPrincipal);
            video.CreatedOn = DateTime.Now;
            video.UpdatedOn = DateTime.Now;
            video.FileName = createViewModel.VideoFile.FileName;

            video = await videoService.Add(video);

            string webRootPath = webHostEnvironment.WebRootPath;
            string pathToVideo = $@"{webRootPath}\UserFiles\{video.Creator.Email}\Videos\{video.Id}\{video.FileName}";

            EnsureFolder(pathToVideo);

            using (var fileStream = new FileStream(pathToVideo, FileMode.Create))
            {
                await createViewModel.VideoFile.CopyToAsync(fileStream);
            }
            return video;
        }

        public async Task<ActionResult<EditViewModel>> UpdateVideo(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal)
        {
            var video = videoService.GetVideo(editViewModel.Video.Id);

            if (video is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, video, Operations.Update);

            if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);

            video.Published = editViewModel.Video.Published;
            video.Title = editViewModel.Video.Title;
            video.Content = editViewModel.Video.Content;
            video.UpdatedOn = DateTime.Now;

            if (editViewModel.VideoFile != null)
            {
                video.FileName = editViewModel.VideoFile.FileName;
                string webRootPath = webHostEnvironment.WebRootPath;
                string pathToVideo = $@"{webRootPath}\UserFiles\{video.Creator.Email}\Videos\{video.Id}\{video.FileName}";



                using (var fileStream = new FileStream(pathToVideo, FileMode.Create))
                {
                    await editViewModel.VideoFile.CopyToAsync(fileStream);
                }
            }

            return new EditViewModel
            {
                Video = await videoService.Update(video)
            };
        }

        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();
            var videoId = id.Value;

            var video = videoService.GetVideo(videoId);

            if (video is null)
                return new NotFoundResult();

            var authorizationResut = await authorizationService.AuthorizeAsync(claimsPrincipal, video, Operations.Update);

            if (!authorizationResut.Succeeded) return DetermineActionResult(claimsPrincipal);
            return new EditViewModel
            {
                Video = video
            };
        }

        private ActionResult DetermineActionResult(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }

        private void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (directoryName.Length > 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }
    }
}
