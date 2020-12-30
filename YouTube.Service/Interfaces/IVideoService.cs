using System.Collections.Generic;
using System.Threading.Tasks;
using YouTube.Data.Models;

namespace YouTube.Service.Interfaces
{
    public interface IVideoService
    {
        Video GetVideo(int videoId);

        Task<Video> Add(Video video);

        Task<Video> Update(Video video);

        IEnumerable<Video> GetVideos(string searchString);

        IEnumerable<Video> GetVideos(ApplicationUser applicationUser);
    }
}
