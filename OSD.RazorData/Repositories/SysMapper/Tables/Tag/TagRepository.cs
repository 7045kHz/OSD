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
    public class TagRepository : ITagRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public TagRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }
        public Tag Add(Tag v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
                    // VALUES (@Name, @Address, @City, @State, @PostalCode);
                    // SELECT CAST(SCOPDE_IDENTITY() as int);
                    // This method returns the ID of the latest insert
                    var sql = "INSERT INTO [dbo].[Tag] (Name, CategoryId,LifeCycleId)  VALUES (@Name, @CategoryId, @LifeCycleId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
                    var id = cnn.Query<int>(sql, v).Single();
                    v.TagId = id;
                    return v;
                }
                catch (Exception e) { throw e; }
            }
        }

        public Tag Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[Tag] (NOLOCK)  WHERE TagId = @TagId";
                    return cnn.Query<Tag>(sql, new { @TagId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }



        public List<Tag> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[Tag] (NOLOCK) ";
                    return cnn.Query<Tag>(sql).ToList();
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
                    var sql = "DELETE FROM [dbo].[Tag] WHERE TagId = @Id";
                    cnn.Execute(sql, new { @Id = id });
                }
                catch (Exception e) { throw e; }
            }

        }

        public Tag Update(Tag v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                    var sql = "UPDATE [dbo].[Tag] SET Name = @Name, CategoryId=@CategoryId,LifeCycleId=@LifeCycleId  "
                    + "WHERE TagId=@TagId;";
                    cnn.Execute(sql, v);
                    return v;
                }
                catch (Exception e) { throw e; }
            }
        }
    }
}
