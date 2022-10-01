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
    public class ResourceMapRepository : IResourceMapRepository
    {
        private IDbConnection db;
        public ResourceMapRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }

        public ResourceMap Add(ResourceMap v)
        {
            // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
            // VALUES (@Name, @Address, @City, @State, @PostalCode);
            // SELECT CAST(SCOPDE_IDENTITY() as int);
            // This method returns the ID of the latest insert
            var sql = "INSERT INTO [dbo].[ResourceMap] (Name, TagId,ResourceTypeId, LabelMapId, LifeCycleId, CategoryId)  VALUES (@Name, @TagId,@ResourceTypeId, @LabelMapId, @LifeCycleId, @CategoryId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql, v).Single();
            v.LifeCycleId = id;
            return v;
        }






        public ResourceMap Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[ResourceMap] WHERE ResourceMapId = @ResourceMapId";
            return db.Query<ResourceMap>(sql, new { @LifecycleId = id }).Single();
        }



        public List<ResourceMap> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[ResourceMap]";
            return db.Query<ResourceMap>(sql).ToList();
        }

        public void Remove(int id)
        {
            // DELETE FROM Companies WHERE CompanyId = @Id
            var sql = "DELETE FROM [dbo].[ResourceMap] WHERE ResourceMapId = @Id";
            db.Execute(sql, new { @Id = id });

        }

        public ResourceMap Update(ResourceMap v)
        {
            // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
            var sql = "UPDATE [dbo].[ResourceType] SET Name = @Name,TagId=@TagId, LabelMapId=@LabelMapId, ResourceTypeId=@ResourceTypeId, LifeCycleId=@LifeCycleId,CategoryId = @CategoryId "
                    + "WHERE ResourceMapId=@ResourceMapID;";
            db.Execute(sql, v);
            return v;
        }
    }
}
