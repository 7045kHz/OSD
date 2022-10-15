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


        public async Task<LifeCycle> AddAsync(LifeCycle life)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
                    // VALUES (@Name, @Address, @City, @State, @PostalCode);
                    // SELECT CAST(SCOPDE_IDENTITY() as int);
                    // This method returns the ID of the latest insert


                    var sql = "INSERT INTO [dbo].[LifeCycle] (Name, CategoryId)  VALUES (@Name, @CategoryId); "
                            + "SELECT CAST(SCOPE_IDENTITY() as int);";
                    var id = (await cnn.QueryAsync<int>(sql, life)).Single();

                    life.LifeCycleId = id;
                    return life;

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed insert");
                life.LifeCycleId = -1;
                throw e;
            }
        }


        public LifeCycle Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[LifeCycle] (NOLOCK) WHERE LifeCycleId = @LifeCycleId";
                    return cnn.Query<LifeCycle>(sql, new { @LifeCycleId = id }).Single();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public async Task<LifeCycle> FindAsync(int id)
        {
            try
            {


                using (var cnn = _context.CreateConnection())
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    Console.WriteLine($"FindAsync id = {id}");
                    var sql = "SELECT * FROM [dbo].[LifeCycle] (NOLOCK) WHERE LifeCycleId = @LifeCycleId";
                    var r = await cnn.QueryAsync<LifeCycle>(sql, new { @LifeCycleId = id });
                    Console.WriteLine($"FindAsync return = {r.Single()}");
                    return r.Single();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int NameExists(string name)
        {
            try
            {


                using (var cnn = _context.CreateConnection())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Name", name);
                    param.Add(
                        name: "@Exists",
                        dbType: DbType.Int32,
                        direction: ParameterDirection.ReturnValue
                    );
                    var sql = "[SYSMAPPER].[dbo].[mapr_LifeCycleExistsName]";
                    var results = cnn.Execute(sql, param, commandType: CommandType.StoredProcedure);
                    Console.WriteLine("SP (ToString) results = " + results.ToString());
                    Console.WriteLine("SP (Raw) results = " + results);
                    var r = param.Get<int>("@Exists");
                    Console.WriteLine("R = " + r);
                    return r;

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<LifeCycle> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[LifeCycle] (NOLOCK)";
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
                    var sql = "SELECT * FROM [dbo].[LifeCycle] (NOLOCK) ";
                    IEnumerable<LifeCycle> results = await cnn.QueryAsync<LifeCycle>(sql);
                    return results.ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public async Task<List<LifeCycle>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction = "DESC")
        {
            try
            {


                using (var cnn = _context.CreateConnection())
                {
                    IEnumerable<LifeCycle> list = await cnn.QueryAsync<LifeCycle>($"select * from [dbo].[LifeCycle] (NOLOCK) ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text);
                    return list.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<LifeCycle>> SearchAsync(int searchId)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[LifeCycle] (NOLOCK) WHERE LifeCycleId = @searchId  ";
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
                    var sql = "SELECT * FROM [dbo].[LifeCycle] (NOLOCK) WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  ";
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
                    var sql = "SELECT * FROM [dbo].[LifeCycle] WHERE (NOLOCK) UPPER(Name)  LIKE CONCAT('%',@SearchString,'%') AND LifeCycleId =  @SearchId ";
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
        public async Task<int> RemoveAsync(int id)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    // DELETE FROM Companies WHERE CompanyId = @Id
                    var sql = "DELETE FROM [dbo].[LifeCycle] WHERE LifeCycleId = @Id";
                    return await cnn.ExecuteAsync(sql, new { @Id = id });
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public LifeCycle Update(LifeCycle v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                    var sql = "UPDATE [dbo].[LifeCycle] SET Name = @Name, CategoryId = @CategoryId "
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
        public async Task<LifeCycle> UpdateAsync(LifeCycle life)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                    var sql = "UPDATE [dbo].[LifeCycle] SET Name = @Name, CategoryId = @CategoryId "
                        + "WHERE LifeCycleId=@LifeCycleId;";
                    await cnn.ExecuteAsync(sql, life);
                    return life;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
