using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface ILifeCycleRepository
    {
        LifeCycle Add(LifeCycle v);
        Task<LifeCycle> AddAsync(LifeCycle v);
        LifeCycle Find(int id);
        Task<LifeCycle> FindAsync(int id);
        List<LifeCycle> GetAll();
        Task<List<LifeCycle>> GetAllAsync();
        Task<List<LifeCycle>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction = "DESC");

        void Remove(int id);
        Task<int> RemoveAsync(int id);
        Task<List<LifeCycle>> SearchAsync(int searchId);
        Task<List<LifeCycle>> SearchAsync(string searchString);
        Task<List<LifeCycle>> SearchAsync(string searchString, int searchId);
        LifeCycle Update(LifeCycle v);
        Task<LifeCycle> UpdateAsync(LifeCycle category);
        int NameExists(string name);
    }
}