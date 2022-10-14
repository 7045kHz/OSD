using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface ICategoryRepository
    {
        Category Add(Category category);
        Task<Category> AddAsync(Category category);
        Category Find(int id);
        Task<Category> FindAsync(int id);
        List<Category> GetAll();
        Task<List<Category>> GetAllAsync();
        Task<List<Category>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction = "DESC");
        int NameExists(string name);
        void Remove(int id);
        Task<int> RemoveAsync(int id);
        Task<List<Category>> SearchAsync(int searchId);
        Task<List<Category>> SearchAsync(string searchString);
        Task<List<Category>> SearchAsync(string searchString, int searchId);
        Category Update(Category category);
        Task<Category> UpdateAsync(Category category);
    }
}