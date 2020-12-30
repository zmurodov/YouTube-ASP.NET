using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTube.Data;
using YouTube.Data.Models;
using YouTube.Service.Interfaces;

namespace YouTube.Service
{
    public class VideoService: IVideoService
    {

        private readonly ApplicationDbContext applicationDbContext;

        public VideoService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public Video GetVideo(int videoId)
        {
            return applicationDbContext.Videos
                .Include(video => video.Creator)
                .Include(video => video.Comments).ThenInclude(comment => comment.Poster)
                .Include(video => video.Comments).ThenInclude(comment => comment.Comments)
                .FirstOrDefault(video => video.Id == videoId);
        } 

        public IEnumerable<Video> GetVideos(string searchString)
        {
            return applicationDbContext.Videos
                .OrderByDescending(video => video.UpdatedOn)
                .Include(video => video.Creator)
                .Include(video => video.Comments)
                .Where(video => video.Title.Contains(searchString) || video.Content.Contains(searchString));
        }

        public IEnumerable<Video> GetVideos(ApplicationUser applicationUser)
        {
            return applicationDbContext.Videos
                .Include(video => video.Creator)
                .Include(video => video.Approver)
                .Include(video => video.Comments)
                .Where(video => video.Creator == applicationUser);
        }

        public async Task<Video> Add(Video video)
        {
            applicationDbContext.Add(video);
            await applicationDbContext.SaveChangesAsync();
            return video;
        }

        public async Task<Video> Update(Video video)
        {
            applicationDbContext.Update(video);
            await applicationDbContext.SaveChangesAsync();
            return video;
        }
    }
}
