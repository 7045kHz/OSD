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

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public class OuRepository : IOuRepository
    {
        private IConfiguration _config { get; set; }
        public string ConnectionString { get; set; }
        public OuRepository(IConfiguration configuration)
        {
            _config = configuration;
            ConnectionString = _config.GetConnectionString("DefaultConnection");
        }
        public Ou Add(Ou v)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
                // VALUES (@Name, @Address, @City, @State, @PostalCode);
                // SELECT CAST(SCOPDE_IDENTITY() as int);
                // This method returns the ID of the latest insert
                try
                {
                    var sql = "INSERT INTO [dbo].[OU] (Team,Organization,CategoryId, LifeCycleId)  VALUES (@Team,@Organization,@CategoryId, @LifeCycleId); "
                            + "SELECT CAST(SCOPE_IDENTITY() as int);";
                    var id = cnn.Query<int>(sql, v).Single();
                    v.OuId = id;
                    return v;
                }
                catch (Exception e)
                {
                    v.OuId = -1;
                    return v;
                }

            }
        }

        public Ou Find(int id)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                // SELECT * FROM Companies WHERE CompanyId = @Id
                var sql = "SELECT * FROM [dbo].[OU] WHERE OuId = @OuId";
                return cnn.Query<Ou>(sql, new { @OuId = id }).Single();
            }
        }
        public Ou Find(Ou v)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                // SELECT * FROM Companies WHERE CompanyId = @Id
                var sql = "SELECT * FROM [dbo].[OU] WHERE OuId = @OuId";
                return cnn.Query<Ou>(sql, new { @OuId = v.OuId }).Single();
            }
        }


        public List<Ou> GetAll()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                //   SELECT * FROM Companies
                var sql = "SELECT * FROM [dbo].[OU]";
                return cnn.Query<Ou>(sql).ToList();
            }
        }
        public async Task<List<Ou>> GetAllAsync()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                //   SELECT * FROM Companies
                var sql = "SELECT * FROM [dbo].[OU]";
                IEnumerable<Ou> results = await cnn.QueryAsync<Ou>(sql);
                return results.ToList();
            }
        }
        public async Task<List<Ou>> SearchAsync(int searchId)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM [dbo].[Ou] WHERE OuId = @searchId  ";
                Console.WriteLine("String: Count: " + searchId);

                IEnumerable<Ou> results = await cnn.QueryAsync<Ou>(sql, new { @searchId = searchId });
                return results.ToList();
            }
        }
        public async Task<List<Ou>> SearchAsync(string searchString)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM [dbo].[Ou] WHERE UPPER(Organization)  LIKE CONCAT('%',@SearchString,'%') OR UPPER(Team)  LIKE CONCAT('%',@SearchString,'%') ";
                Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                IEnumerable<Ou> results = await cnn.QueryAsync<Ou>(sql, new { @SearchString = searchString.ToUpper() });
                return results.ToList();
            }
        }
        public async Task<List<Ou>> SearchAsync(string searchString, int searchId)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM [dbo].[Ou] WHERE  (UPPER(Organization)  LIKE CONCAT('%',@SearchString,'%') OR UPPER(Team)  LIKE CONCAT('%',@SearchString,'%')) AND LifeCycleId =  @SearchId ";
                Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                IEnumerable<Ou> results = await cnn.QueryAsync<Ou>(sql, new { @SearchString = searchString.ToUpper(), @SearchId = searchId });
                return results.ToList();
            }
        }
        public void Remove(int id)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                // DELETE FROM Companies WHERE CompanyId = @Id
                var sql = "DELETE FROM [dbo].[OU] WHERE OuId = @Id";
                cnn.Execute(sql, new { @Id = id });
            }

        }

        public Ou Update(Ou v)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                var sql = "UPDATE [dbo].[OU] SET Team = @Team, Organization=@Organization,CategoryId=@CategoryId, LifeCycleId=@LifeCycleId "
                    + "WHERE OuId=@OuId;";
                cnn.Execute(sql, v);
                return v;

            }
        }
    }
}
