using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface IOuRepository
    {
        Ou Add(Ou v);
        Task<Ou> AddAsync(Ou v);
        Ou Find(int id);
        Ou Find(Ou v);
        Task<Ou> FindAsync(int id);
        List<Ou> GetAll();
        Task<List<Ou>> GetAllAsync();
        int NameExists(string name);
        void Remove(int id);
        Task<int> RemoveAsync(int id);
        Task<List<Ou>> SearchAsync(int searchId);
        Task<List<Ou>> SearchAsync(string searchString);
        Task<List<Ou>> SearchAsync(string searchString, int searchId);
        Ou Update(Ou v);
        Task<Ou> UpdateAsync(Ou v);
    }
}