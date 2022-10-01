using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface IOuRepository
    {
        Ou Add(Ou v);
        Ou Find(int id);
        Ou Find(Ou v);
        List<Ou> GetAll();
        void Remove(int id);
        Ou Update(Ou v);
    }
}