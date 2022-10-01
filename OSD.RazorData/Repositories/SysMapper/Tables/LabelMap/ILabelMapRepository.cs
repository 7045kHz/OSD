using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface ILabelMapRepository
    {
        LabelMap Add(LabelMap v);
        LabelMap Find(int id);
        List<LabelMap> GetAll();
        void Remove(int id);
        LabelMap Update(LabelMap v);
    }
}