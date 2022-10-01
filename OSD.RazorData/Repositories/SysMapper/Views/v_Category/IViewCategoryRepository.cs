using OSD.RazorData.Models.SysMapper.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public interface IViewCategoryRepository
    {
        VCategory Find(int id);
        List<VCategory> GetAll();
        Task<List<VCategory>> GetAllAsync();
        List<VCategory> Search(string searchString);
        Task<List<VCategory>> SearchAsync(string searchString);
    }
}