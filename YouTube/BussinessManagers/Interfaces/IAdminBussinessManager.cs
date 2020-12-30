using System.Security.Claims;
using System.Threading.Tasks;
using YouTube.Models.AdminViewModels;

namespace YouTube.BussinessManagers.Interfaces
{
    public interface IAdminBussinessManager
    {
        Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal);

        Task<AboutViewModel> GetAboutViewModel(ClaimsPrincipal claimsPrincipal);

        Task UpdateAbout(AboutViewModel aboutViewModel, ClaimsPrincipal claimsPrincipal);
    }
}
