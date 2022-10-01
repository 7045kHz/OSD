using OSD.RazorData.Models.SysMapper.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public interface IViewResourceTypeRepository
    {
        VResourceType Find(int id);
        List<VResourceType> GetAll();
        Task<List<VResourceType>> GetAllAsync();
        List<VResourceType> Search(string searchString);
        Task<List<VResourceType>> SearchAsync(string searchString);
    }
}