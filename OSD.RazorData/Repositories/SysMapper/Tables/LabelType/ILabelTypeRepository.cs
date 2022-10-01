using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface ILabelTypeRepository
    {
        LabelType Add(LabelType v);
        LabelType Find(int id);
        List<LabelType> GetAll();
        void Remove(int id);
        LabelType Update(LabelType v);
    }
}