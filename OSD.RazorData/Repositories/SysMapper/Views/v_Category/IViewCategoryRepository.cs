using OSD.RazorData.Models.SysMapper.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public interface IViewCategoryRepository
    {
        Task<List<VCategory>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction);
        Task<List<VCategory>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction, string searchString);
        int Count();
        int Count(string searchString);

        VCategory Find(int id);
        List<VCategory> GetAll();
        Task<List<VCategory>> GetAllAsync();
        List<VCategory> Search(string searchString);
        Task<List<VCategory>> SearchAsync(string searchString);
    }
}