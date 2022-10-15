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
    public class ResourceMapRepository : IResourceMapRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public ResourceMapRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }

        public ResourceMap Add(ResourceMap v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
                    // VALUES (@Name, @Address, @City, @State, @PostalCode);
                    // SELECT CAST(SCOPDE_IDENTITY() as int);
                    // This method returns the ID of the latest insert
                    var sql = "INSERT INTO [dbo].[ResourceMap] (Name, TagId,ResourceTypeId, LabelMapId, LifeCycleId, CategoryId)  VALUES (@Name, @TagId,@ResourceTypeId, @LabelMapId, @LifeCycleId, @CategoryId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
                    var id = cnn.Query<int>(sql, v).Single();
                    v.LifeCycleId = id;
                    return v;
                }
                catch (Exception e) { throw e; }
            }
        }






        public ResourceMap Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[ResourceMap] (NOLOCK)  WHERE ResourceMapId = @ResourceMapId";
                    return cnn.Query<ResourceMap>(sql, new { @LifecycleId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }



        public List<ResourceMap> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[ResourceMap] (NOLOCK)";
                    return cnn.Query<ResourceMap>(sql).ToList();
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
                    var sql = "DELETE FROM [dbo].[ResourceMap] WHERE ResourceMapId = @Id";
                    cnn.Execute(sql, new { @Id = id });
                }
                catch (Exception e) { throw e; }
            }

        }

        public ResourceMap Update(ResourceMap v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                    var sql = "UPDATE [dbo].[ResourceType] SET Name = @Name,TagId=@TagId, LabelMapId=@LabelMapId, ResourceTypeId=@ResourceTypeId, LifeCycleId=@LifeCycleId,CategoryId = @CategoryId "
                    + "WHERE ResourceMapId=@ResourceMapID;";
                    cnn.Execute(sql, v);
                    return v;
                }
                catch (Exception e) { throw e; }
            }
        }
    }
}
