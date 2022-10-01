using OSD.RazorData.Models.SysMapper.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public interface IViewOuRepository
    {
        VOu Find(int id);
        Task<VOu> FindAsync(int id);
        List<VOu> GetAll();
        Task<List<VOu>> GetAllAsync();
        List<VOu> Search(string searchString);
        Task<List<VOu>> SearchAsync(string searchString);
    }
}