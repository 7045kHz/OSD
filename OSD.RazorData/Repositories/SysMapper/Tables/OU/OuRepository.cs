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
    public class OuRepository : IOuRepository
    {
        private readonly DapperSysMapperDbContext _context;

        public OuRepository(DapperSysMapperDbContext context)
        {
            _context = context;
        }
        public Ou Add(Ou v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "INSERT INTO [dbo].[OU] (Organization,CategoryId, LifeCycleId)  VALUES (@Organization,@CategoryId, @LifeCycleId); "
                            + "SELECT CAST(SCOPE_IDENTITY() as int);";
                    var id = cnn.Query<int>(sql, v).Single();
                    v.OuId = id;
                    return v;
                }
                catch (Exception e)
                {
                    v.OuId = -1;
                    return v;
                }

            }
        }

        public Ou Find(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[OU] (NOLOCK) WHERE OuId = @OuId";
                    return cnn.Query<Ou>(sql, new { @OuId = id }).Single();
                } catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public Ou Find(Ou v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // SELECT * FROM Companies WHERE CompanyId = @Id
                    var sql = "SELECT * FROM [dbo].[OU] (NOLOCK) WHERE OuId = @OuId";
                    return cnn.Query<Ou>(sql, new { @OuId = v.OuId }).Single();
                } catch (Exception e ) { throw e; }

            }
        }


        public List<Ou> GetAll()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[OU] (NOLOCK) ";
                    return cnn.Query<Ou>(sql).ToList();
                } catch (Exception e) { throw e; }
            }
        }
        public async Task<List<Ou>> GetAllAsync()
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    //   SELECT * FROM Companies
                    var sql = "SELECT * FROM [dbo].[OU] (NOLOCK)";
                    IEnumerable<Ou> results = await cnn.QueryAsync<Ou>(sql);
                    return results.ToList();
                }catch (Exception e) { throw e; }
            }
        }
        public async Task<List<Ou>> SearchAsync(int searchId)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[Ou] (NOLOCK) WHERE OuId = @searchId  ";
                    Console.WriteLine("String: Count: " + searchId);

                    IEnumerable<Ou> results = await cnn.QueryAsync<Ou>(sql, new { @searchId = searchId });
                    return results.ToList();
                }catch(Exception e) { throw e; }
            }
        }
        public async Task<List<Ou>> SearchAsync(string searchString)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[Ou] (NOLOCK)  WHERE UPPER(Organization)  LIKE CONCAT('%',@SearchString,'%') ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<Ou> results = await cnn.QueryAsync<Ou>(sql, new { @SearchString = searchString.ToUpper() });
                    return results.ToList();
                }catch(Exception ex) { throw ex; }
            }
        }
        public async Task<List<Ou>> SearchAsync(string searchString, int searchId)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM [dbo].[Ou] (NOLOCK) WHERE  (UPPER(Organization)  LIKE CONCAT('%',@SearchString,'%') ) AND LifeCycleId =  @SearchId ";
                    Console.WriteLine("String: Count: " + searchString.Count() + " String Value: " + searchString);

                    IEnumerable<Ou> results = await cnn.QueryAsync<Ou>(sql, new { @SearchString = searchString.ToUpper(), @SearchId = searchId });
                    return results.ToList();
                }catch (Exception ex) { throw ex; }
            }
        }
        public void Remove(int id)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // DELETE FROM Companies WHERE CompanyId = @Id
                    var sql = "DELETE FROM [dbo].[OU] WHERE OuId = @Id";
                    cnn.Execute(sql, new { @Id = id });
                }catch (Exception ex) { throw ex; }
            }

        }

        public Ou Update(Ou v)
        {
            using (var cnn = _context.CreateConnection())
            {
                try
                {
                    // UPDATE Companies SET Name = @Name, Address=@Address, City=@City, PostalCode=@PostalCode WHERE CompanyID=@CompanyId;
                    var sql = "UPDATE [dbo].[OU] SET  Organization=@Organization,CategoryId=@CategoryId, LifeCycleId=@LifeCycleId "
                    + "WHERE OuId=@OuId;";
                    cnn.Execute(sql, v);
                    return v;
                }catch (Exception ex) { throw ex; }

            }
        }
    }
}
