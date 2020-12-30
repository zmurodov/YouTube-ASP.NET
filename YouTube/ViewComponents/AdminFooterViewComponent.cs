﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace YouTube.ViewComponents
{
    public class AdminFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Factory.StartNew(() => { return View(); });
        }
    }
}
