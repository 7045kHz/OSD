﻿using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface ICategoryRepository
    {
        string ConnectionString { get; set; }
        Task<List<Category>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction  );

        Category Add(Category category);
        Task<Category> AddAsync(Category category);
        Category Find(int id);
        List<Category> GetAll();
        Task<List<Category>> GetAllAsync();
        int NameExists(string name);
        void Remove(int id);
        Task<List<Category>> SearchAsync(int searchId);
        Task<List<Category>> SearchAsync(string searchString);
        Task<List<Category>> SearchAsync(string searchString, int searchId);
        Category Update(Category category);
    }
}