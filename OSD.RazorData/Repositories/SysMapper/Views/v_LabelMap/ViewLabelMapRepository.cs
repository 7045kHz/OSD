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
    public class ViewLabelMapRepository : IViewLabelMapRepository
    {
        private IDbConnection db;
        public ViewLabelMapRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }

        public VLabelMap Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[v_LabelMap] WHERE LabelMapId = @LabelMapId";
            return db.Query<VLabelMap>(sql, new { @LabelMapId = id }).Single();
        }

        public List<VLabelMap> Search(string searchString)
        {
            var sql = "SELECT * FROM [dbo].[v_LabelMap] WHERE UPPER(TagName)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%') ";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VLabelMap> results = db.Query<VLabelMap>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();

        }
        public async Task<List<VLabelMap>> SearchAsync(string searchString)
        {

            var sql = "SELECT * FROM [dbo].[v_LabelMap] WHERE UPPER(TagName)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%') ";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VLabelMap> results = await db.QueryAsync<VLabelMap>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();
        }

        public List<VLabelMap> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_LabelMap]";
            return db.Query<VLabelMap>(sql).ToList();
        }
        public async Task<List<VLabelMap>> GetAllAsync()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_LabelMap]";
            IEnumerable<VLabelMap> results = await db.QueryAsync<VLabelMap>(sql);
            return results.ToList();
        }
    }
}
