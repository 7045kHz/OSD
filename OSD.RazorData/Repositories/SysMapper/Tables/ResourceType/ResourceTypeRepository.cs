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
    public class ResourceTypeRepository : IResourceTypeRepository
    {
        private IDbConnection db;
        public ResourceTypeRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }

        public ResourceType Add(ResourceType v)
        {
            // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
            // VALUES (@Name, @Address, @City, @State, @PostalCode);
            // SELECT CAST(SCOPDE_IDENTITY() as int);
            // This method returns the ID of the latest insert
            var sql = "INSERT INTO [dbo].[ResourceType] (Name, Vendor,Product, LifeCycleId, CategoryId)  VALUES (@Name, @Vendor,@Product, @LifeCycleId, @CategoryId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql, v).Single();
            v.LifeCycleId = id;
            return v;
        }






        public ResourceType Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[ResourceType] WHERE ResourceTypeId = @ResourceTypeId";
            return db.Query<ResourceType>(sql, new { @LifecycleId = id }).Single();
        }



        public List<ResourceType> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[ResourceType]";
            return db.Query<ResourceType>(sql).ToList();
        }

        public void Remove(int id)
        {
            // DELETE FROM Companies WHERE CompanyId = @Id
            var sql = "DELETE FROM [dbo].[ResourceType] WHERE ResourceTypeId = @Id";
            db.Execute(sql, new { @Id = id });

        }

        public ResourceType Update(ResourceType v)
        {
            // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
            var sql = "UPDATE [dbo].[ResourceType] SET Name = @Name,Product=@Product, Vendor=@Vendor, LifeCycleId=@LifeCycleId,CategoryId = @CategoryId "
                    + "WHERE ResourceTypeID=@ResourceTypeId;";
            db.Execute(sql, v);
            return v;
        }
    }
}
