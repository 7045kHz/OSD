using Microsoft.EntityFrameworkCore;
using OSD.RazorData.Context;


namespace OSD.RazorData.Context
{
    public class SysmapperDbContext : DbContext
    {
        public SysmapperDbContext(DbContextOptions<SysmapperDbContext> options) : base(options)
        {

        }
    }
}
