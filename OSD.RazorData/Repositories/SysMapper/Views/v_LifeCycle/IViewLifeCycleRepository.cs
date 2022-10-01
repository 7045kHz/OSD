using OSD.RazorData.Models.SysMapper.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public interface IViewLifeCycleRepository
    {
        VLifeCycle Find(int id);
        List<VLifeCycle> GetAll();
        Task<List<VLifeCycle>> GetAllAsync();
        List<VLifeCycle> Search(string searchString);
        Task<List<VLifeCycle>> SearchAsync(string searchString);
    }
}