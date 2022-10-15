
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public CategoryRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }

        public Category Add(Category category)
        {
            using (var cnn = _context.CreateConnection())
            {
                // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
                // VALUES (@Name, @Address, @City, @State, @PostalCode);
                // SELECT CAST(SCOPDE_IDENTITY() as int);
                // This method returns the ID of the latest insert
                try
                {
                    {
                        var sql = "INSERT INTO [dbo].[Category] (Name, LifeCycleId)  VALUES (@Name, @LifeCycleId); "
                                + "SELECT CAST(SCOPE_IDENTITY() as int);";
                        var id = cnn.Query<int>(sql, category).Single();

                        category.CategoryId = id;
                        return category;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed insert");

                    throw e;
                }
            }
        }

        public async Task<Category> AddAsync(Category category)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
                    // VALUES (@Name, @Address, @City, @State, @PostalCode);
                    // SELECT CAST(SCOPDE_IDENTITY() as int);
                    // This method returns the ID of the latest insert


                    var sql = "INSERT INTO [dbo].[Category] (Name, LifeCycleId)  VALUES (@Name, @LifeCycleId); "
                            + "SELECT CAST(SCOPE_IDENTITY() as int);";
                    var id = (await cnn.QueryAsync<int>(sql, category)).Single();

                    category.CategoryId = id;
                    return category;

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed insert");
                category.CategoryId = -1;
                throw e;
            }
        }




        public Category Find(int id)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[Category] (NOLOCK) WHERE CategoryId = @CategoryId";
                    return cnn.Query<Category>(sql, new { @CategoryId = id }).Single();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Category> FindAsync(int id)
        {
            try
            {


                using (var cnn = _context.CreateConnection())
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    Console.WriteLine($"FindAsync id = {id}");
                    var sql = "SELECT * FROM [dbo].[Category] (NOLOCK) WHERE CategoryId = @CategoryId";
                    var r = await cnn.QueryAsync<Category>(sql, new { @CategoryId = id });
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
                    var sql = "[SYSMAPPER].[dbo].[mapr_CategoryExistsName]";
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
        public List<Category> GetAll()
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[Category] (NOLOCK) ";
                    return cnn.Query<Category>(sql).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<Category>> GetAllAsync()
        {
            try
            {


                using (var cnn = _context.CreateConnection())
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[Category] (NOLOCK)";
                    IEnumerable<Category> results = await cnn.QueryAsync<Category>(sql);
                    return results.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<Category>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction = "DESC")
        {
            try
            {


                using (var cnn = _context.CreateConnection())
                {
                    IEnumerable<Category> list = await cnn.QueryAsync<Category>($"select * from [dbo].[Category] (NOLOCK) ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text);
                    return list.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Category>> SearchAsync(int searchId)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    var sql = "SELECT * FROM [dbo].[Category] (NOLOCK) WHERE LifeCycleId = @searchId  ";
                    Console.WriteLine("String: Count: " + searchId);

                    IEnumerable<Category> results = await cnn.QueryAsync<Category>(sql, new { @searchId = searchId });
                    return results.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<Category>> SearchAsync(string searchString)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    var sql = "SELECT * FROM [dbo].[Category] (NOLOCK)  WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<Category> results = await cnn.QueryAsync<Category>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<Category>> SearchAsync(string searchString, int searchId)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    var sql = "SELECT * FROM [dbo].[Category] (NOLOCK) WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%') AND LifeCycleId =  @SearchId ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<Category> results = await cnn.QueryAsync<Category>(sql, new { @SearchString = searchString.ToUpper(), @SearchId = searchId });
                    return results.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Remove(int id)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    // DELETE FROM Companies WHERE CompanyId = @Id
                    var sql = "DELETE FROM [dbo].[Category] WHERE CategoryId = @Id";
                    cnn.Execute(sql, new { @Id = id });
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task<int> RemoveAsync(int id)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    // DELETE FROM Companies WHERE CompanyId = @Id
                    var sql = "DELETE FROM [dbo].[Category] WHERE CategoryId = @Id";
                    return await cnn.ExecuteAsync(sql, new { @Id = id });
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public Category Update(Category category)
        {
            try
            {


                using (var cnn = _context.CreateConnection())
                {
                    // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                    var sql = "UPDATE [dbo].[Category] SET Name = @Name, LifeCycleId = @LifeCycleId "
                        + "WHERE CategoryId=@CategoryId;";
                    cnn.Execute(sql, category);
                    return category;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Category> UpdateAsync(Category category)
        {
            try
            {
                using (var cnn = _context.CreateConnection())
                {
                    // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                    var sql = "UPDATE [dbo].[Category] SET Name = @Name, LifeCycleId = @LifeCycleId "
                        + "WHERE CategoryId=@CategoryId;";
                    await cnn.ExecuteAsync(sql, category);
                    return category;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
