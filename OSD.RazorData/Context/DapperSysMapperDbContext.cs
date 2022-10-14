using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace OSD.RazorData.Context
{
    public class DapperSysMapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperSysMapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SysMapperConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
