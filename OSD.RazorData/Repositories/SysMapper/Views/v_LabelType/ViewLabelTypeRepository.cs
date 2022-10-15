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
    public class ViewLabelTypeRepository : IViewLabelTypeRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public ViewLabelTypeRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }


        public VLabelType Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[v_LabelType] (NOLOCK)  WHERE LabelTypeId = @LabelTypeId";
                    return cnn.Query<VLabelType>(sql, new { @LabelTypeId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }

        public List<VLabelType> Search(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_LabelType] (NOLOCK)  WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(Organization) LIKE CONCAT('%',@SearchString,'%') ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VLabelType> results = cnn.Query<VLabelType>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }

        }
        public async Task<List<VLabelType>> SearchAsync(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[v_LabelType] (NOLOCK)  WHERE UPPER(Name)  LIKE CONCAT('%',@SearchString,'%')  OR UPPER(LifeCycleName) LIKE CONCAT('%',@SearchString,'%') OR UPPER(CategoryName) LIKE CONCAT('%',@SearchString,'%')  OR UPPER(Organization) LIKE CONCAT('%',@SearchString,'%') ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<VLabelType> results = await cnn.QueryAsync<VLabelType>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public List<VLabelType> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_LabelType] (NOLOCK) ";
                    return cnn.Query<VLabelType>(sql).ToList();
                }
                catch (Exception e) { throw e; }
            }
        }

        public async Task<List<VLabelType>> GetAllAsync()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[v_LabelType] (NOLOCK) ";
                    IEnumerable<VLabelType> results = await cnn.QueryAsync<VLabelType>(sql);
                    return results.ToList();
                }
                catch (Exception e) { throw e; }
            }
        }
    }
}
