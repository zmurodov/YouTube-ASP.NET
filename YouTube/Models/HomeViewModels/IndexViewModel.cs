﻿using YouTube.Data.Models;
using PagedList.Core;

namespace YouTube.Models.HomeViewModel
{
    public class IndexViewModel
    { 
        public IPagedList<Video> Videos{ get; set; }

        public string SearchString { get; set; }

        public int PageNumber { get; set; }
    }
}
