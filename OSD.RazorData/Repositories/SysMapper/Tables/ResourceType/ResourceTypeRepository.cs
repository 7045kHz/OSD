using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OSD.RazorData.Models.SysMapper.Tables;
using OSD.RazorData.Models.SysMapper.Views;
using OSD.RazorData.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public class ResourceTypeRepository : IResourceTypeRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public ResourceTypeRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }

        public ResourceType Add(ResourceType v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
                    // VALUES (@Name, @Address, @City, @State, @PostalCode);
                    // SELECT CAST(SCOPDE_IDENTITY() as int);
                    // This method returns the ID of the latest insert
                    var sql = "INSERT INTO [dbo].[ResourceType] (Name, Vendor,Product, LifeCycleId, CategoryId)  VALUES (@Name, @Vendor,@Product, @LifeCycleId, @CategoryId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
                    var id = cnn.Query<int>(sql, v).Single();
                    v.LifeCycleId = id;
                    return v;
                }
                catch (Exception e) { throw e; }
            }
        }






        public ResourceType Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[ResourceType] (NOLOCK) WHERE ResourceTypeId = @ResourceTypeId";
                    return cnn.Query<ResourceType>(sql, new { @LifecycleId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }



        public List<ResourceType> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[ResourceType] (NOLOCK)";
                    return cnn.Query<ResourceType>(sql).ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public void Remove(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // DELETE FROM Companies WHERE CompanyId = @Id
                    var sql = "DELETE FROM [dbo].[ResourceType] WHERE ResourceTypeId = @Id";
                    cnn.Execute(sql, new { @Id = id });
                }
                catch (Exception e) { throw e; }
            }

        }

        public ResourceType Update(ResourceType v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                    var sql = "UPDATE [dbo].[ResourceType] SET Name = @Name,Product=@Product, Vendor=@Vendor, LifeCycleId=@LifeCycleId,CategoryId = @CategoryId "
                    + "WHERE ResourceTypeID=@ResourceTypeId;";
                    cnn.Execute(sql, v);
                    return v;
                }
                catch (Exception e) { throw e; }
            }
        }
    }
}
