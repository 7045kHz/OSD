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
    public class ViewLifeCycleRepository : IViewLifeCycleRepository
    {
        private IDbConnection db;
        public ViewLifeCycleRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }








        public VLifeCycle Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[v_LifeCycle] WHERE CategoryId = @CategoryId";
            return db.Query<VLifeCycle>(sql, new { @LifecycleId = id }).Single();
        }

        public List<VLifeCycle> Search(string searchString)
        {
            var sql = "SELECT * FROM [dbo].[v_LifeCycle] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%') OR  UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')   ";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VLifeCycle> results = db.Query<VLifeCycle>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();

        }
        public async Task<List<VLifeCycle>> SearchAsync(string searchString)
        {

            var sql = "SELECT * FROM [dbo].[v_LifeCycle] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%') OR  UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')   ";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VLifeCycle> results = await db.QueryAsync<VLifeCycle>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();
        }

        public List<VLifeCycle> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_LifeCycle]";
            return db.Query<VLifeCycle>(sql).ToList();
        }

        public async Task<List<VLifeCycle>> GetAllAsync()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_LifeCycle]";
            IEnumerable<VLifeCycle> results = await db.QueryAsync<VLifeCycle>(sql);
            return results.ToList();
        }




    }
}
