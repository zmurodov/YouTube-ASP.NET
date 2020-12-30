using System.Threading.Tasks;
using YouTube.Data.Models;

namespace YouTube.Service.Interfaces
{
    public interface IUserService
    {
        ApplicationUser Get(string id);

        Task<ApplicationUser> Update(ApplicationUser applicationUser);
    }
}
