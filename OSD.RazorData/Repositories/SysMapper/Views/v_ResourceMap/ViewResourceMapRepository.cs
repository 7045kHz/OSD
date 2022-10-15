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
    public class ViewResourceMapRepository : IViewResourceMapRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public ViewResourceMapRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }


        public VResourceMap Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[v_ResourceMap] (NOLOCK)  WHERE ResourceMapId = @ResourceMapId";
                    return cnn.Query<VResourceMap>(sql, new { @LifecycleId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }



        public List<VResourceMap> Search(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_ResourceMap] (NOLOCK)  WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(TagName) LIKE CONCAT('%',@SearchString,'%')   OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(ResourceName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Product) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Vendor) LIKE CONCAT('%',@SearchString,'%')";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VResourceMap> results = cnn.Query<VResourceMap>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }

        }
        public async Task<List<VResourceMap>> SearchAsync(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_ResourceMap] (NOLOCK)  WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(TagName) LIKE CONCAT('%',@SearchString,'%')   OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(ResourceName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Product) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Vendor) LIKE CONCAT('%',@SearchString,'%')";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VResourceMap> results = await cnn.QueryAsync<VResourceMap>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public List<VResourceMap> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_ResourceMap] (NOLOCK) ";
                    return cnn.Query<VResourceMap>(sql).ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public async Task<List<VResourceMap>> GetAllAsync()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_ResourceMap] (NOLOCK) ";
                    IEnumerable<VResourceMap> results = await cnn.QueryAsync<VResourceMap>(sql);
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

    }
}
