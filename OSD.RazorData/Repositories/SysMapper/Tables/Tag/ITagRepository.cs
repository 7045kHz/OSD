using OSD.RazorData.Models.SysMapper.Tables;
using System.Collections.Generic;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public interface ITagRepository
    {
        Tag Add(Tag v);
        Tag Find(int id);
        List<Tag> GetAll();
        void Remove(int id);
        Tag Update(Tag v);
    }
}