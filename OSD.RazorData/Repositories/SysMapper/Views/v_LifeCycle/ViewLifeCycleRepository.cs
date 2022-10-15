using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OSD.RazorData.Models.SysMapper.Tables;
using OSD.RazorData.Models.SysMapper.Views;
using OSD.RazorData.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public class ViewLifeCycleRepository : IViewLifeCycleRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public ViewLifeCycleRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }

        public VLifeCycle Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[v_LifeCycle] (NOLOCK) WHERE CategoryId = @CategoryId";
                    return cnn.Query<VLifeCycle>(sql, new { @LifecycleId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }

        public List<VLifeCycle> Search(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_LifeCycle] (NOLOCK) WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%') OR  UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')   ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VLifeCycle> results = cnn.Query<VLifeCycle>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }

        }
        public async Task<List<VLifeCycle>> SearchAsync(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_LifeCycle] (NOLOCK) WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%') OR  UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')   ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VLifeCycle> results = await cnn.QueryAsync<VLifeCycle>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public List<VLifeCycle> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_LifeCycle] (NOLOCK)";
                    return cnn.Query<VLifeCycle>(sql).ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public async Task<List<VLifeCycle>> GetAllAsync()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_LifeCycle] (NOLOCK)";
                    IEnumerable<VLifeCycle> results = await cnn.QueryAsync<VLifeCycle>(sql);
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }




    }
}
