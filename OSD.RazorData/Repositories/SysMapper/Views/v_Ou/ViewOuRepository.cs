using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OSD.RazorData.Models.SysMapper.Tables;
using OSD.RazorData.Models.SysMapper.Views;
using OSD.RazorData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public class ViewOuRepository : IViewOuRepository
    {
        private IDbConnection db;
        public ViewOuRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }


        public VOu Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[v_OU] WHERE OuId = @OuId";
            return db.Query<VOu>(sql, new { @OuId = id }).Single();
        }
        public async Task<VOu> FindAsync(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[v_OU] WHERE OuId = @OuId";
            return (await db.QueryAsync<VOu>(sql, new { @OuId = id })).Single();
        }
        public List<VOu> Search(string searchString)
        {
            var sql = "SELECT * FROM [dbo].[v_OU] WHERE UPPER(Team)  LIKE CONCAT('%',@SearchString,'%') OR UPPER(Organization) LIKE CONCAT('%',@SearchString,'%') OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<VOu> results =   db.Query<VOu>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();

        }
        public async Task<List<VOu>> SearchAsync(string searchString)
        {
            
            var sql = "SELECT * FROM [dbo].[v_OU] WHERE UPPER(Team)  LIKE CONCAT('%',@SearchString,'%') OR UPPER(Organization) LIKE CONCAT('%',@SearchString,'%') OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);
   
            IEnumerable<VOu> results = await db.QueryAsync<VOu>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();
        }

        public List<VOu> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_OU]";
            return db.Query<VOu>(sql).ToList();
        }
        public async Task<List<VOu>> GetAllAsync()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[v_OU]";
            IEnumerable<VOu> results = await db.QueryAsync<VOu>(sql);
            return results.ToList();
        }

    }
}
