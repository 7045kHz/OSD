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

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public class OuRepository : IOuRepository
    {
        private IDbConnection db;
        public OuRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }
        public Ou Add(Ou v)
        {
            // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
            // VALUES (@Name, @Address, @City, @State, @PostalCode);
            // SELECT CAST(SCOPDE_IDENTITY() as int);
            // This method returns the ID of the latest insert
            var sql = "INSERT INTO [dbo].[OU] (Team,Organization,CategoryId, LifeCycleId)  VALUES (@Team,@Organization,@CategoryId, @LifeCycleId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql, v).Single();
            v.OuId = id;
            return v;
        }

        public Ou Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[OU] WHERE OuId = @OuId";
            return db.Query<Ou>(sql, new { @OuId = id }).Single();
        }
        public Ou Find(Ou v)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[OU] WHERE OuId = @OuId";
            return db.Query<Ou>(sql, new { @OuId = v.OuId }).Single();
        }


        public List<Ou> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[OU]";
            return db.Query<Ou>(sql).ToList();
        }

        public void Remove(int id)
        {
            // DELETE FROM Companies WHERE CompanyId = @Id
            var sql = "DELETE FROM [dbo].[OU] WHERE OuId = @Id";
            db.Execute(sql, new { @Id = id });

        }

        public Ou Update(Ou v)
        {
            // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
            var sql = "UPDATE [dbo].[OU] SET Team = @Team, Organization=@Organization,CategoryId=@CategoryId, LifeCycleId=@LifeCycleId "
                    + "WHERE OuId=@OuId;";
            db.Execute(sql, v);
            return v;
        }
    }
}
