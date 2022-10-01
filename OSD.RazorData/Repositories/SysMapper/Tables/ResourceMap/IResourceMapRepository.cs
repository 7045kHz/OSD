using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface IResourceMapRepository
    {
        ResourceMap Add(ResourceMap v);
        ResourceMap Find(int id);
        List<ResourceMap> GetAll();
        void Remove(int id);
        ResourceMap Update(ResourceMap v);
    }
}