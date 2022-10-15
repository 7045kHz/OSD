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

namespace OSD.RazorData.Repositories.SysMapper.Views
{
    public class ViewCategoryRepository : IViewCategoryRepository
    {

        private readonly DapperSysMapperDbContext _context;

        public ViewCategoryRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }

        public VCategory Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[v_Category] (NOLOCK) WHERE CategoryId = @CategoryId";
                    return cnn.Query<VCategory>(sql, new { @CompanyId = id }).Single();
                }
                catch (Exception e) { throw e; }
            
            }
        }
        public int Count()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    return cnn.Query<int>("select COUNT(*) from [dbo].[v_Category] (NOLOCK) ", null, commandType: CommandType.Text).Single();
                }catch (Exception e) { throw e; }
            }
        }
        public int Count(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    return cnn.Query<int>("select COUNT(*) from [dbo].[v_Category] (NOLOCK) WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%')  ", new { @SearchString = searchString.ToUpper() }, commandType: CommandType.Text).Single();
                }catch(Exception e) { throw e; }
            }
        }
        public async Task<List<VCategory>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction = "DESC" )
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    IEnumerable<VCategory> list = await cnn.QueryAsync<VCategory>($"select * from [dbo].[v_Category] (NOLOCK)  ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text);
                    return list.ToList();
                }catch(Exception e) { throw e; }
            }

        }
        public async Task<List<VCategory>> GetAllAsyncOrder(int skip, int take, string orderBy, string direction = "DESC", string searchString="")
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    IEnumerable<VCategory> list = await cnn.QueryAsync<VCategory>($"select * from [dbo].[v_Category] (NOLOCK) WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%')  ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", new { @SearchString = searchString.ToUpper() }, commandType: CommandType.Text);
                    return list.ToList();
                }catch (Exception e) { throw e; }
            }

        }

        public List<VCategory> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_Category] (NOLOCK)";
                    return cnn.Query<VCategory>(sql).ToList();
                }catch(Exception e) { throw e; }
            }
        }
        public async Task<List<VCategory>> GetAllAsync()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_Category] (NOLOCK)";
                    IEnumerable<VCategory> results = await cnn.QueryAsync<VCategory>(sql);
                    return results.ToList();
                }catch(Exception e) { throw e; }
            }
        }
        public List<VCategory> Search(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_Category] (NOLOCK) WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VCategory> results = cnn.Query<VCategory>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }catch (Exception e) { throw e; }
            }

        }
        public async Task<List<VCategory>> SearchAsync(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_Category] (NOLOCK) WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VCategory> results = await cnn.QueryAsync<VCategory>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }catch(Exception e) { throw e; }
            }
        }
    }
}
