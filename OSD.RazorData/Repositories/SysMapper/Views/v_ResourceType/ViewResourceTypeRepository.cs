using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OSD.RazorData.Models.SysMapper.Tables;
using OSD.RazorData.Models.SysMapper.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public class ViewResourceTypeRepository : IViewResourceTypeRepository
    {
        private IDbConnection db;
        public ViewResourceTypeRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }







        public VResourceType Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[v_ResourceType] WHERE ResourceTypeId = @ResourceTypeId";
            return db.Query<VResourceType>(sql, new { @LifecycleId = id }).Single();
        }



        public List<VResourceType> Search(string searchString)
        {
            var sql = "SELECT * FROM [dbo].[v_ResourceType] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')   OR UPPER(Product) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Vendor) LIKE CONCAT('%',@SearchString,'%')";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VResourceType> results = db.Query<VResourceType>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();

        }
        public async Task<List<VResourceType>> SearchAsync(string searchString)
        {

            var sql = "SELECT * FROM [dbo].[v_ResourceType] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')   OR UPPER(Product) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Vendor) LIKE CONCAT('%',@SearchString,'%')";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VResourceType> results = await db.QueryAsync<VResourceType>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();
        }

        public List<VResourceType> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_ResourceType]";
            return db.Query<VResourceType>(sql).ToList();
        }

        public async Task<List<VResourceType>> GetAllAsync()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_ResourceType]";
            IEnumerable<VResourceType> results = await db.QueryAsync<VResourceType>(sql);
            return results.ToList();
        }


    }
}
