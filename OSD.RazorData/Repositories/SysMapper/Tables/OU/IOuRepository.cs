using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface IOuRepository
    {
        Ou Add(Ou v);
        Ou Find(int id);
        Ou Find(Ou v);
        List<Ou> GetAll();
        Task<List<Ou>> GetAllAsync();
        void Remove(int id);
        Task<List<Ou>> SearchAsync(int searchId);
        Task<List<Ou>> SearchAsync(string searchString);
        Task<List<Ou>> SearchAsync(string searchString, int searchId);
        Ou Update(Ou v);
    }
}