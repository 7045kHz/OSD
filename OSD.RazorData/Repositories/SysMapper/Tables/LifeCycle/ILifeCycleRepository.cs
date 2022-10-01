using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface ILifeCycleRepository
    {
        LifeCycle Add(LifeCycle v);
        LifeCycle Find(int id);
        List<LifeCycle> GetAll();
        Task<List<LifeCycle>> GetAllAsync();
        void Remove(int id);
        Task<List<LifeCycle>> SearchAsync(int searchId);
        Task<List<LifeCycle>> SearchAsync(string searchString);
        Task<List<LifeCycle>> SearchAsync(string searchString, int searchId);
        LifeCycle Update(LifeCycle v);
    }
}