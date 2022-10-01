using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface IResourceTypeRepository
    {
        ResourceType Add(ResourceType v);
        ResourceType Find(int id);
        List<ResourceType> GetAll();
        void Remove(int id);
        ResourceType Update(ResourceType v);
    }
}