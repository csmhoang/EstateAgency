using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure;

public class MyContext
{
    public IDbConnection Connection { get; }
    public MyContext(IConfiguration config)
    {
        Connection = new SqlConnection(config.GetConnectionString("sqlserver"));
    }
}
