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
    public class ViewLabelMapRepository : IViewLabelMapRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public ViewLabelMapRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }

        public VLabelMap Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[v_LabelMap] (NOLOCK) WHERE LabelMapId = @LabelMapId";
                    return cnn.Query<VLabelMap>(sql, new { @LabelMapId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }

        public List<VLabelMap> Search(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_LabelMap] (NOLOCK) WHERE UPPER(TagName)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%') ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VLabelMap> results = cnn.Query<VLabelMap>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }

        }
        public async Task<List<VLabelMap>> SearchAsync(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_LabelMap] (NOLOCK) WHERE UPPER(TagName)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%') ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VLabelMap> results = await cnn.QueryAsync<VLabelMap>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public List<VLabelMap> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_LabelMap] (NOLOCK)";
                    return cnn.Query<VLabelMap>(sql).ToList();
                }
                catch (Exception e) { throw e; }
            }
        }
        public async Task<List<VLabelMap>> GetAllAsync()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_LabelMap] (NOLOCK)";
                    IEnumerable<VLabelMap> results = await cnn.QueryAsync<VLabelMap>(sql);
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }
    }
}
