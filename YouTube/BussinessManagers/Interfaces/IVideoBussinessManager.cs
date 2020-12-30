using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YouTube.Data.Models;
using YouTube.Models.HomeViewModel;
using YouTube.Models.VideoViewModels;

namespace YouTube.BussinessManagers.Interfaces
{
    public interface IVideoBussinessManager
    {

        Task<Video> CreateVideo(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);

        Task<ActionResult<VideoViewModel>> GetVideoViewModel(int? id, ClaimsPrincipal claimsPrincipal);

        IndexViewModel GetIndexViewModel(string searchString, int? page);

        Task<ActionResult<EditViewModel>> UpdateVideo(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal);
    }
}
