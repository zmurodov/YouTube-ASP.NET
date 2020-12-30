using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTube.BussinessManagers.Interfaces;
using YouTube.Data.Models;
using YouTube.Models.HomeViewModel;
using YouTube.Service.Interfaces;

namespace YouTube.BussinessManagers
{
    public class HomeBussinessManager: IHomeBussinessManager
    {

        private readonly IVideoService videoService;
        private readonly IUserService userService;

        public HomeBussinessManager(IVideoService videoService, IUserService userService)
        {
            this.videoService = videoService;
            this.userService = userService;
        }

        public ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string searchString, int? page)
        {
            if (authorId is null)
                return new BadRequestResult();

            var applicationUser = userService.Get(authorId);

            if (applicationUser is null)
                return new NotFoundResult();

            int pageSize = 20;
            int pageNumber = page ?? 1;

            var videos = videoService.GetVideos(searchString ?? string.Empty)
                .Where(video => video.Published && video.Creator == applicationUser);

            return new AuthorViewModel
            {
                Author = applicationUser,
                Videos = new StaticPagedList<Video>(videos.Skip((pageNumber - 1) * pageSize).Take(pageSize),
                            pageNumber, pageSize, videos.Count()),
                SearchString = searchString,
                PageNumber = pageNumber
            };
        }
    }
}
