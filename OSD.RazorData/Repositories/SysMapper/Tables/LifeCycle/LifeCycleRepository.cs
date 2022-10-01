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

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public class LifeCycleRepository : ILifeCycleRepository
    {
        private IDbConnection db;
        public LifeCycleRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }

        public LifeCycle Add(LifeCycle v)
        {
            // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
            // VALUES (@Name, @Address, @City, @State, @PostalCode);
            // SELECT CAST(SCOPDE_IDENTITY() as int);
            // This method returns the ID of the latest insert
            var sql = "INSERT INTO [dbo].[LifeCycle] (Name, CategoryId)  VALUES (@Name, @CategoryId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql, v).Single();
            v.LifeCycleId = id;
            return v;
        }






        public LifeCycle Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[LifeCycle] WHERE CategoryId = @CategoryId";
            return db.Query<LifeCycle>(sql, new { @LifecycleId = id }).Single();
        }



        public List<LifeCycle> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[LifeCycle]";
            return db.Query<LifeCycle>(sql).ToList();
        }
        public async Task<List<LifeCycle>> GetAllAsync()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[LifeCycle]";
            IEnumerable<LifeCycle> results = await db.QueryAsync<LifeCycle>(sql);
            return results.ToList();
        }
        public async Task<List<LifeCycle>> SearchAsync(int searchId)
        {

            var sql = "SELECT * FROM [dbo].[LifeCycle] WHERE LifeCycleId = @searchId  ";
            Console.WriteLine("String: Count: " + searchId);

            IEnumerable<LifeCycle> results = await db.QueryAsync<LifeCycle>(sql, new { @searchId = searchId });
            return results.ToList();
        }
        public async Task<List<LifeCycle>> SearchAsync(string searchString)
        {

            var sql = "SELECT * FROM [dbo].[LifeCycle] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  ";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<LifeCycle> results = await db.QueryAsync<LifeCycle>(sql, new { @SearchString = searchString.ToUpper() });
            return results.ToList();
        }
        public async Task<List<LifeCycle>> SearchAsync(string searchString, int searchId)
        {

            var sql = "SELECT * FROM [dbo].[LifeCycle] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%') AND LifeCycleId =  @SearchId ";
            Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

            IEnumerable<LifeCycle> results = await db.QueryAsync<LifeCycle>(sql, new { @SearchString = searchString.ToUpper(), @SearchId = searchId });
            return results.ToList();
        }
        public void Remove(int id)
        {
            // DELETE FROM Companies WHERE CompanyId = @Id
            var sql = "DELETE FROM [dbo].[LifeCycle] WHERE LifeCycleId = @Id";
            db.Execute(sql, new { @Id = id });

        }

        public LifeCycle Update(LifeCycle v)
        {
            // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
            var sql = "UPDATE [dbo].[Category] SET Name = @Name, CategoryId = @CategoryId "
                    + "WHERE LifeCycleId=@LifeCycleId;";
            db.Execute(sql, v);
            return v;
        }
    }
}
