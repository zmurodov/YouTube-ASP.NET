using YouTube.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTube.Models.AdminViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Video> Videos { get; set; }
    }
}
