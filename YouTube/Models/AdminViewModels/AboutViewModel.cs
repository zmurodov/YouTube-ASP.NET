using YouTube.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace YouTube.Models.AdminViewModels
{
    public class AboutViewModel
    {
        public ApplicationUser Author { get; set; }

        [Display(Name="Header Image")]
        public IFormFile HeaderImage { get; set; }

        [Display(Name ="Sub Header")]
        public string SubHeader { get; set; }

        public string Content { get; set; }
    }
}
