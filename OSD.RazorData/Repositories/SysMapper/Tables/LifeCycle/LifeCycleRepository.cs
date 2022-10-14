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
    public class LifeCycleRepository : ILifeCycleRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public LifeCycleRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }

        public LifeCycle Add(LifeCycle v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "INSERT INTO [dbo].[LifeCycle] (Name, CategoryId)  VALUES (@Name, @CategoryId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
                    var id = cnn.Query<int>(sql, v).Single();
                    v.LifeCycleId = id;
                    return v;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }





        public LifeCycle Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[LifeCycle] WHERE CategoryId = @CategoryId";
                    return cnn.Query<LifeCycle>(sql, new { @LifecycleId = id }).Single();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }



        public List<LifeCycle> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[LifeCycle]";
                    return cnn.Query<LifeCycle>(sql).ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public async Task<List<LifeCycle>> GetAllAsync()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[LifeCycle]";
                    IEnumerable<LifeCycle> results = await cnn.QueryAsync<LifeCycle>(sql);
                    return results.ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public async Task<List<LifeCycle>> SearchAsync(int searchId)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[LifeCycle] WHERE LifeCycleId = @searchId  ";
                    Console.WriteLine("String: Count: " + searchId);

                    IEnumerable<LifeCycle> results = await cnn.QueryAsync<LifeCycle>(sql, new { @searchId = searchId });
                    return results.ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public async Task<List<LifeCycle>> SearchAsync(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[LifeCycle] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<LifeCycle> results = await cnn.QueryAsync<LifeCycle>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public async Task<List<LifeCycle>> SearchAsync(string searchString, int searchId)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[LifeCycle] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%') AND LifeCycleId =  @SearchId ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<LifeCycle> results = await cnn.QueryAsync<LifeCycle>(sql, new { @SearchString = searchString.ToUpper(), @SearchId = searchId });
                    return results.ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public void Remove(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // DELETE FROM Companies WHERE CompanyId = @Id
                    var sql = "DELETE FROM [dbo].[LifeCycle] WHERE LifeCycleId = @Id";
                    cnn.Execute(sql, new { @Id = id });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public LifeCycle Update(LifeCycle v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                    var sql = "UPDATE [dbo].[Category] SET Name = @Name, CategoryId = @CategoryId "
                        + "WHERE LifeCycleId=@LifeCycleId;";
                    cnn.Execute(sql, v);
                    return v;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
