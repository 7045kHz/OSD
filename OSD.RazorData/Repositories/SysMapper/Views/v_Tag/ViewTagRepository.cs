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
    public class ViewTagRepository : IViewTagRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public ViewTagRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }


        public VTag Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[v_Tag] (NOLOCK)  WHERE TagId = @TagId";
                    return cnn.Query<VTag>(sql, new { @TagId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }



        public List<VTag> Search(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_Tag] (NOLOCK)  WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')    OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%') ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VTag> results = cnn.Query<VTag>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }

        }
        public async Task<List<VTag>> SearchAsync(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_Tag] (NOLOCK)  WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')    OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%') ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VTag> results = await cnn.QueryAsync<VTag>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public List<VTag> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_Tag] (NOLOCK) ";
                    return cnn.Query<VTag>(sql).ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public async Task<List<VTag>> GetAllAsync()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_Tag] (NOLOCK) ";
                    IEnumerable<VTag> results = await cnn.QueryAsync<VTag>(sql);
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

    }
}
