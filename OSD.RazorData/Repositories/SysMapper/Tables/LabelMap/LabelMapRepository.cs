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
    public class LabelMapRepository : ILabelMapRepository
    {
        private IDbConnection db;
        public LabelMapRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }



        public LabelMap Add(LabelMap v)
        {
            // INSERT INTO Companies (Name, Address,City,State,PostalCode) 
            // VALUES (@Name, @Address, @City, @State, @PostalCode);
            // SELECT CAST(SCOPDE_IDENTITY() as int);
            // This method returns the ID of the latest insert
            var sql = "INSERT INTO [dbo].[LabelMap] (Name,CategoryId,LifeCycleId)  VALUES (@Name,  @CategoryId,@LifeCycleId); "
                    + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql, v).Single();
            v.LabelMapId = id;
            return v;
        }



        public LabelMap Find(int id)
        {
            // SELECT * FROM Companies WHERE CompanyId = @Id
            var sql = "SELECT * FROM [dbo].[LabelMap] WHERE LabelMapId = @LabelMapId";
            return db.Query<LabelMap>(sql, new { @LabelMapId = id }).Single();
        }



        public List<LabelMap> GetAll()
        {
            //   SELECT * FROM Companies
            var sql = "SELECT * FROM [dbo].[LabelMap]";
            return db.Query<LabelMap>(sql).ToList();
        }

        public void Remove(int id)
        {
            // DELETE FROM Companies WHERE CompanyId = @Id
            var sql = "DELETE FROM [dbo].[LabelMap] WHERE LabelMapId = @Id";
            db.Execute(sql, new { @Id = id });

        }

        public LabelMap Update(LabelMap v)
        {
            // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
            var sql = "UPDATE [dbo].[LabelMap] SET LabelTypeId = @LabelTypeId, LifeCycleId=@LifeCycleID,CategoryId=@CategoryId, TagId=@TagId "
                    + "WHERE LabelMapId=@LabelMapId;";
            db.Execute(sql, v);
            return v;
        }
    }
}
