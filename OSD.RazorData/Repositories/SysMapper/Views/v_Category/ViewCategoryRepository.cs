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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public class ViewCategoryRepository : IViewCategoryRepository
    {
        private IConfiguration _config { get; set; }
        public string ConnectionString { get; set; }
        public ViewCategoryRepository(IConfiguration configuration)
        {
            _config = configuration;
            ConnectionString = _config.GetConnectionString("DefaultConnection");
        }


        public VCategory Find(int id)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                // SELECT * FROM Companies WHERE CompanyId = @Id
                var sql = "SELECT * FROM [dbo].[v_Category] WHERE CategoryId = @CategoryId";
                return cnn.Query<VCategory>(sql, new { @CompanyId = id }).Single();
            }
        }
        public int Count()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                return cnn.Query<int>("select COUNT(*) from [dbo].[v_Category] ", null, commandType: CommandType.Text).Single();

            }
        }
        public int Count(string searchString)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                return cnn.Query<int>("select COUNT(*) from [dbo].[v_Category] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%')  ", new {@SearchString = searchString.ToUpper() }, commandType: CommandType.Text).Single();

            }
        }
        public async Task<List<VCategory>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction = "DESC" )
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                IEnumerable<VCategory> list = await cnn.QueryAsync<VCategory>($"select * from [dbo].[v_Category] ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ",null, commandType: CommandType.Text);
                return list.ToList();
            }

        }
        public async Task<List<VCategory>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction = "DESC", string searchString="")
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                IEnumerable<VCategory> list = await cnn.QueryAsync<VCategory>($"select * from [dbo].[v_Category] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%')  ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", new { @SearchString = searchString.ToUpper() }, commandType: CommandType.Text);
                return list.ToList();
            }

        }

        public List<VCategory> GetAll()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                //   SELECT * FROM Companies
                var sql = "SELECT * FROM [dbo].[v_Category]";
                return cnn.Query<VCategory>(sql).ToList();
            }
        }
        public async Task<List<VCategory>> GetAllAsync()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                //   SELECT * FROM Companies
                var sql = "SELECT * FROM [dbo].[v_Category]";
                IEnumerable<VCategory> results = await cnn.QueryAsync<VCategory>(sql);
                return results.ToList();
            }
        }
        public List<VCategory> Search(string searchString)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM [dbo].[v_Category] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') ";
                Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                IEnumerable<VCategory> results = cnn.Query<VCategory>(sql, new { @SearchString = searchString.ToUpper() });
                return results.ToList();
            }

        }
        public async Task<List<VCategory>> SearchAsync(string searchString)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM [dbo].[v_Category] WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') ";
                Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                IEnumerable<VCategory> results = await cnn.QueryAsync<VCategory>(sql, new { @SearchString = searchString.ToUpper() });
                return results.ToList();
            }
        }
    }
}
