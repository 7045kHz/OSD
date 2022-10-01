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
    public class ViewResourceMapRepository : IViewResourceMapRepository
    {
        private IDbConnection db;
        public ViewResourceMapRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }


        public VResourceMap Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[v_ResourceMap] WHERE ResourceMapId = @ResourceMapId";
            return db.Query<VResourceMap>(sql, new { @LifecycleId = id }).Single();
        }



        public List<VResourceMap> Search(string searchString)
        {
            var sql = "SELECT * FROM [dbo].[v_ResourceMap] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(TagName) LIKE CONCAT('%',@SearchString,'%')   OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(ResourceName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Product) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Vendor) LIKE CONCAT('%',@SearchString,'%')";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VResourceMap> results = db.Query<VResourceMap>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();

        }
        public async Task<List<VResourceMap>> SearchAsync(string searchString)
        {

            var sql = "SELECT * FROM [dbo].[v_ResourceMap] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(TagName) LIKE CONCAT('%',@SearchString,'%')   OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(ResourceName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Product) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Vendor) LIKE CONCAT('%',@SearchString,'%')";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VResourceMap> results = await db.QueryAsync<VResourceMap>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();
        }

        public List<VResourceMap> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_ResourceMap]";
            return db.Query<VResourceMap>(sql).ToList();
        }

        public async Task<List<VResourceMap>> GetAllAsync()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_ResourceMap]";
            IEnumerable<VResourceMap> results = await db.QueryAsync<VResourceMap>(sql);
            return results.ToList();
        }

    }
}
