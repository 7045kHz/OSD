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
    public class ViewCategoryRepository : IViewCategoryRepository
    {
        private IDbConnection db;
        public ViewCategoryRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }


        public VCategory Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[v_Category] WHERE CategoryId = @CategoryId";
            return db.Query<VCategory>(sql, new { @CompanyId = id }).Single();
        }



        public List<VCategory> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_Category]";
            return db.Query<VCategory>(sql).ToList();
        }
        public async Task<List<VCategory>> GetAllAsync()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_Category]";
            IEnumerable<VCategory> results = await db.QueryAsync<VCategory>(sql);
            return results.ToList();
        }
        public List<VCategory> Search(string searchString)
        {
            var sql = "SELECT * FROM [dbo].[v_Category] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') ";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VCategory> results = db.Query<VCategory>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();

        }
        public async Task<List<VCategory>> SearchAsync(string searchString)
        {

            var sql = "SELECT * FROM [dbo].[v_Category] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') ";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VCategory> results = await db.QueryAsync<VCategory>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();
        }
    }
}
