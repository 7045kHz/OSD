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
    public class ViewLabelTypeRepository : IViewLabelTypeRepository
    {
        private IDbConnection db;
        public ViewLabelTypeRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }


        public VLabelType Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[v_LabelType] WHERE LabelTypeId = @LabelTypeId";
            return db.Query<VLabelType>(sql, new { @LabelTypeId = id }).Single();
        }

        public List<VLabelType> Search(string searchString)
        {
            var sql = "SELECT * FROM [dbo].[v_LabelType] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(Organization) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Team) LIKE CONCAT('%',@SearchString,'%')";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VLabelType> results = db.Query<VLabelType>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();

        }
        public async Task<List<VLabelType>> SearchAsync(string searchString)
        {

            var sql = "SELECT * FROM [dbo].[v_LabelType] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(Organization) LIKE CONCAT('%',@SearchString,'%') OR UPPER(Team) LIKE CONCAT('%',@SearchString,'%')";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VLabelType> results = await db.QueryAsync<VLabelType>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();
        }

        public List<VLabelType> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_LabelType]";
            return db.Query<VLabelType>(sql).ToList();
        }

        public async Task<List<VLabelType>> GetAllAsync()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_LabelType]";
            IEnumerable<VLabelType> results = await db.QueryAsync<VLabelType>(sql);
            return results.ToList();
        }
    }
}
