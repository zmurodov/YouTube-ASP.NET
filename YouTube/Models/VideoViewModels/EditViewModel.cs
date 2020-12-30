using YouTube.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace YouTube.Models.VideoViewModels
{
    public class EditViewModel
    {
        [Display(Name = "Vide File")]
        public IFormFile VideoFile { get; set; }

        public Video Video { get; set; }
    }
}
