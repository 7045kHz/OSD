using OSD.RazorData.Models.SysMapper.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public interface IViewTagRepository
    {
        VTag Find(int id);
        List<VTag> GetAll();
        Task<List<VTag>> GetAllAsync();
        List<VTag> Search(string searchString);
        Task<List<VTag>> SearchAsync(string searchString);
    }
}