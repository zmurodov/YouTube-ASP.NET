using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using YouTube.Data.Models;

namespace YouTube.Models.VideoViewModels
{
    [IgnoreAntiforgeryToken]
    public class CreateViewModel
    {
        [Required, Display(Name = "Video file")]
        public IFormFile VideoFile { get; set; }

        public Video Video { get; set; }

    }
}
