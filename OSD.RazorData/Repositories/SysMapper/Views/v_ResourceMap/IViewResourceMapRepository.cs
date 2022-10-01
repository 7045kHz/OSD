using OSD.RazorData.Models.SysMapper.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public interface IViewResourceMapRepository
    {
        VResourceMap Find(int id);
        List<VResourceMap> GetAll();
        Task<List<VResourceMap>> GetAllAsync();
        List<VResourceMap> Search(string searchString);
        Task<List<VResourceMap>> SearchAsync(string searchString);
    }
}