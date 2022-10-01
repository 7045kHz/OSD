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

namespace OSD.RazorData.Repositories.SysMapper.Tables
{
    public class LabelTypeRepository : ILabelTypeRepository
    {
        private IDbConnection db;
        public LabelTypeRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }
        public LabelType Add(LabelType v)
        {
            // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
            // VALUES (@Name, @Address, @City, @State, @PostalCode);
            // SELECT CAST(SCOPDE_IDENTITY() as int);
            // This method returns the ID of the latest insert
            var sql = "INSERT INTO [dbo].[LabelType] (Name, CategoryId,OuId,LifeCycleId)  VALUES (@Name, @CategoryId,@OuId, @LifeCycleId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql, v).Single();
            v.CategoryId = id;
            return v;
        }

        public LabelType Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[LabelType] WHERE LabelTypeId = @LabelTypeId";
            return db.Query<LabelType>(sql, new { @LabelTypeId = id }).Single();
        }



        public List<LabelType> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[LabelType]";
            return db.Query<LabelType>(sql).ToList();
        }

        public void Remove(int id)
        {
            // DELETE FROM Companies WHERE CompanyId = @Id
            var sql = "DELETE FROM [dbo].[LabelType] WHERE LabelTypeId = @Id";
            db.Execute(sql, new { @Id = id });

        }

        public LabelType Update(LabelType v)
        {
            // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
            var sql = "UPDATE [dbo].[LabelType] SET Name = @Name, LifeCycleId=@LifeCycleId,CategoryId=@CategoryId, OuId=@OuId "
                    + "WHERE LabelTypeId=@LabelTypeId;";
            db.Execute(sql, v);
            return v;
        }
    }
}
