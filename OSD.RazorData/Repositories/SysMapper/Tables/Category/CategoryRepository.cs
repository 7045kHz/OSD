
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OSD.RazorData.Models.SysMapper.Tables;
using OSD.RazorData.Models.SysMapper.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public class CategoryRepository : ICategoryRepository
    {
        private IConfiguration _config { get; set; }
        public string ConnectionString { get; set; }
        public CategoryRepository(IConfiguration configuration)
        {
            _config = configuration;
            ConnectionString = _config.GetConnectionString("DefaultConnection");
        }

        public Category Add(Category category)
        {
            using (var cnn = new SqlConnection(ConnectionString))
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
                    category.CategoryId = -1;
                    return category;
                }
            }
        }

        public async Task<Category> AddAsync(Category category)
        {
            using (var cnn = new SqlConnection(ConnectionString))
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
                        var id = (await cnn.QueryAsync<int>(sql, category)).Single();

                        category.CategoryId = id;
                        return category;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed insert");
                    category.CategoryId = -1;
                    return category;
                }
            }
        }




        public Category Find(int id)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                // SELECT * FROM Companies WHERE CompanyId = @Id
                var sql = "SELECT * FROM [dbo].[Category] WHERE CategoryId = @CategoryId";
                return cnn.Query<Category>(sql, new { @CompanyId = id }).Single();
            }
        }


        public int NameExists(string name)
        {
            using (var cnn = new SqlConnection(ConnectionString))
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
                Console.WriteLine("SP (Raw) results = " + results );
                var r =  param.Get<int>("@Exists");
                Console.WriteLine("R = " + r);
                return r;

            }
        }
        public List<Category> GetAll()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                //   SELECT * FROM Companies
                var sql = "SELECT * FROM [dbo].[Category]";
                return cnn.Query<Category>(sql).ToList();
            }
        }
        public async Task<List<Category>> GetAllAsync()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                //   SELECT * FROM Companies
                var sql = "SELECT * FROM [dbo].[Category]";
                IEnumerable<Category> results = await cnn.QueryAsync<Category>(sql);
                return results.ToList();
            }
        }
        public async Task<List<Category>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction = "DESC")
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                IEnumerable<Category> list = await cnn.QueryAsync<Category>($"select * from [dbo].[Category] ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text);
                return list.ToList();
            }

        }

        public async Task<List<Category>> SearchAsync(int searchId)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM [dbo].[Category] WHERE LifeCycleId = @searchId  ";
                Console.WriteLine("String: Count: " + searchId);

                IEnumerable<Category> results = await cnn.QueryAsync<Category>(sql, new { @searchId = searchId });
                return results.ToList();
            }
        }
        public async Task<List<Category>> SearchAsync(string searchString)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM [dbo].[Category] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  ";
                Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                IEnumerable<Category> results = await cnn.QueryAsync<Category>(sql, new { @SearchString = searchString.ToUpper() });
                return results.ToList();
            }
        }
        public async Task<List<Category>> SearchAsync(string searchString, int searchId)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM [dbo].[Category] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%') AND LifeCycleId =  @SearchId ";
                Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                IEnumerable<Category> results = await cnn.QueryAsync<Category>(sql, new { @SearchString = searchString.ToUpper(), @SearchId = searchId });
                return results.ToList();
            }
        }

        public void Remove(int id)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                // DELETE FROM Companies WHERE CompanyId = @Id
                var sql = "DELETE FROM [dbo].[Category] WHERE CategoryId = @Id";
                cnn.Execute(sql, new { @Id = id });
            }

        }

        public Category Update(Category category)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                var sql = "UPDATE [dbo].[Category] SET Name = @Name, LifeCycleId = @LifeCycleId "
                    + "WHERE CategoryId=@CategoryId;";
                cnn.Execute(sql, category);
                return category;
            }
        }
    }
}
