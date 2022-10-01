using OSD.RazorData.Models.SysMapper.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public interface IViewLabelMapRepository
    {
        VLabelMap Find(int id);
        List<VLabelMap> GetAll();
        Task<List<VLabelMap>> GetAllAsync();
        List<VLabelMap> Search(string searchString);
        Task<List<VLabelMap>> SearchAsync(string searchString);
    }
}