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
    public class LabelTypeRepository : ILabelTypeRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public LabelTypeRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }
        public LabelType Add(LabelType v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
                    // VALUES (@Name, @Address, @City, @State, @PostalCode);
                    // SELECT CAST(SCOPDE_IDENTITY() as int);
                    // This method returns the ID of the latest insert
                    var sql = "INSERT INTO [dbo].[LabelType] (Name, CategoryId,OuId,LifeCycleId)  VALUES (@Name, @CategoryId,@OuId, @LifeCycleId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
                    var id = cnn.Query<int>(sql, v).Single();
                    v.CategoryId = id;
                    return v;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public LabelType Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[LabelType] (NO-LOCK) WHERE LabelTypeId = @LabelTypeId";
                    return cnn.Query<LabelType>(sql, new { @LabelTypeId = id }).Single();
                }
                catch (Exception e) { throw e; }
            }
        }



        public List<LabelType> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[LabelType] (NO-LOCK)";
                    return cnn.Query<LabelType>(sql).ToList();
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
                    var sql = "DELETE FROM [dbo].[LabelType] WHERE LabelTypeId = @Id";
                    cnn.Execute(sql, new { @Id = id });
                }
                catch (Exception e) { throw e; }
            }

        }

        public LabelType Update(LabelType v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                    var sql = "UPDATE [dbo].[LabelType] SET Name = @Name, LifeCycleId=@LifeCycleId,CategoryId=@CategoryId, OuId=@OuId "
                    + "WHERE LabelTypeId=@LabelTypeId;";
                    cnn.Execute(sql, v);
                    return v;
                }
                catch (Exception e) { throw e; }
            }
        }
    }
}
