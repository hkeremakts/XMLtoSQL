using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLtoSQL.SQL
{
    public class SQLDelete
    {
        public void DeleteIfTableExists(DataTable dt, SqlConnection connection)
        {
            SqlCommand cmd =
                new SqlCommand("IF OBJECT_ID('dbo." + dt.TableName + "', 'U') IS NOT NULL DROP TABLE dbo." + dt.TableName + ";", connection);
            cmd.ExecuteNonQuery();
        }
    }
}
