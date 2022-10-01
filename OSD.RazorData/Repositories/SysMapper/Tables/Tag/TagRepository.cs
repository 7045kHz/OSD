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

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public class TagRepository : ITagRepository
    {
        private IDbConnection db;
        public TagRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }
        public Tag Add(Tag v)
        {
            // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
            // VALUES (@Name, @Address, @City, @State, @PostalCode);
            // SELECT CAST(SCOPDE_IDENTITY() as int);
            // This method returns the ID of the latest insert
            var sql = "INSERT INTO [dbo].[Tag] (Name, CategoryId,LifeCycleId)  VALUES (@Name, @CategoryId, @LifeCycleId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql, v).Single();
            v.TagId = id;
            return v;
        }

        public Tag Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[Tag] WHERE TagId = @TagId";
            return db.Query<Tag>(sql, new { @TagId = id }).Single();
        }



        public List<Tag> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[Tag]";
            return db.Query<Tag>(sql).ToList();
        }

        public void Remove(int id)
        {
            // DELETE FROM Companies WHERE CompanyId = @Id
            var sql = "DELETE FROM [dbo].[Tag] WHERE TagId = @Id";
            db.Execute(sql, new { @Id = id });

        }

        public Tag Update(Tag v)
        {
            // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
            var sql = "UPDATE [dbo].[Tag] SET Name = @Name, CategoryId=@CategoryId,LifeCycleId=@LifeCycleId  "
                    + "WHERE TagId=@TagId;";
            db.Execute(sql, v);
            return v;
        }
    }
}
