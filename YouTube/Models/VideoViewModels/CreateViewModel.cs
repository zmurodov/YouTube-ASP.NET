using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using YouTube.Data.Models;

namespace YouTube.Models.VideoViewModels
{
    public class CreateViewModel
    {
        [Required, Display(Name = "Video file")]
        public IFormFile VideoFile { get; set; }

        public Video Video { get; set; }

    }
}
