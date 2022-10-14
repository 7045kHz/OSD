using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace OSD.RazorData.Context
{
    public class DapperOSDISCOVERYDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperOSDISCOVERYDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OSDiscoveryConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
