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
    public class ViewResourceTypeRepository : IViewResourceTypeRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public ViewResourceTypeRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }


        public VResourceType Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[v_ResourceType] (NOLOCK)  WHERE ResourceTypeId = @ResourceTypeId";
                    return cnn.Query<VResourceType>(sql, new { @LifecycleId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }



        public List<VResourceType> Search(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_ResourceType] (NOLOCK)  WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')   OR UPPER(Product) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Vendor) LIKE CONCAT('%',@SearchString,'%')";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VResourceType> results = cnn.Query<VResourceType>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }

        }
        public async Task<List<VResourceType>> SearchAsync(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_ResourceType] (NOLOCK)  WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')   OR UPPER(Product) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Vendor) LIKE CONCAT('%',@SearchString,'%')";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VResourceType> results = await cnn.QueryAsync<VResourceType>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public List<VResourceType> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_ResourceType] (NOLOCK) ";
                    return cnn.Query<VResourceType>(sql).ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public async Task<List<VResourceType>> GetAllAsync()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_ResourceType] (NOLOCK) ";
                    IEnumerable<VResourceType> results = await cnn.QueryAsync<VResourceType>(sql);
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }


    }
}
