using Microsoft.AspNetCore.Mvc;
using YouTube.Models.HomeViewModel;

namespace YouTube.BussinessManagers.Interfaces
{
    public interface IHomeBussinessManager
    {
        ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string searchString, int? page);

    }
}
