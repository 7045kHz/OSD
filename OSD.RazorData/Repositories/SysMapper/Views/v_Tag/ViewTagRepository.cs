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
    public class ViewTagRepository : IViewTagRepository
    {
        private IDbConnection db;
        public ViewTagRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }


        public VTag Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[v_Tag] WHERE TagId = @TagId";
            return db.Query<VTag>(sql, new { @TagId = id }).Single();
        }



        public List<VTag> Search(string searchString)
        {
            var sql = "SELECT * FROM [dbo].[v_Tag] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')    OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%') ";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VTag> results = db.Query<VTag>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();

        }
        public async Task<List<VTag>> SearchAsync(string searchString)
        {

            var sql = "SELECT * FROM [dbo].[v_Tag] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')    OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%') ";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VTag> results = await db.QueryAsync<VTag>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();
        }

        public List<VTag> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_Tag]";
            return db.Query<VTag>(sql).ToList();
        }

        public async Task<List<VTag>> GetAllAsync()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_Tag]";
            IEnumerable<VTag> results = await db.QueryAsync<VTag>(sql);
            return results.ToList();
        }

    }
}
