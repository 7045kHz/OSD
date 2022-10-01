using OSD.RazorData.Models.SysMapper.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public interface IViewLabelTypeRepository
    {
        VLabelType Find(int id);
        List<VLabelType> GetAll();
        Task<List<VLabelType>> GetAllAsync();
        List<VLabelType> Search(string searchString);
        Task<List<VLabelType>> SearchAsync(string searchString);
    }
}