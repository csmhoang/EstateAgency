using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MyContext
    {
        public IDbConnection Connection { get; }
        public MyContext(IConfiguration config)
        {
            Connection = new SqlConnection(config.GetConnectionString("sqlserver"));
        }
    }
}
