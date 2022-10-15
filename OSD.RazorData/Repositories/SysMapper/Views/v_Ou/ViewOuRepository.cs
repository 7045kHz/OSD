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
    public class ViewOuRepository : IViewOuRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public ViewOuRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }


        public VOu Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[v_OU] (NOLOCK)  WHERE OuId = @OuId";
                    return cnn.Query<VOu>(sql, new { @OuId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }
        public async Task<VOu> FindAsync(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[v_OU] (NOLOCK)  WHERE OuId = @OuId";
                    return (await cnn.QueryAsync<VOu>(sql, new { @OuId = id })).Single();
                }
                catch (Exception e) { throw e; }
            }
        }
        public List<VOu> Search(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_OU] (NOLOCK)  WHERE  UPPER(Organization) LIKE CONCAT('%',@SearchString,'%') OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VOu> results = cnn.Query<VOu>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }

        }
        public async Task<List<VOu>> SearchAsync(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_OU] (NOLOCK)  WHERE  UPPER(Organization) LIKE CONCAT('%',@SearchString,'%') OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VOu> results = await cnn.QueryAsync<VOu>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public List<VOu> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_OU] (NOLOCK) ";
                    return cnn.Query<VOu>(sql).ToList();
                }
                catch (Exception e) { throw e; }
            }
        }
        public async Task<List<VOu>> GetAllAsync()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_OU] (NOLOCK) ";
                    IEnumerable<VOu> results = await cnn.QueryAsync<VOu>(sql);
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

    }
}
