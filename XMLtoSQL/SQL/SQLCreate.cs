using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLtoSQL.SQL
{
    public class SQLCreate
    {
        public string CreateTableQuery(DataTable table)
        {
            string sqlsc = "CREATE TABLE " + table.TableName + "(";
            foreach (DataColumn column in table.Columns)
            {
                sqlsc += "[" + column.ColumnName + "]";
                string columnType = column.DataType.ToString();
                switch (columnType)
                {
                    case "System.Int32":
                        sqlsc += " int ";
                        break;
                    case "System.Int64":
                        sqlsc += " bigint ";
                        break;
                    case "System.Int16":
                        sqlsc += " smallint";
                        break;
                    case "System.Byte":
                        sqlsc += " tinyint";
                        break;
                    case "System.Decimal":
                        sqlsc += " decimal ";
                        break;
                    case "System.DateTime":
                        sqlsc += " datetime ";
                        break;
                    case "System.String":
                    default:
                        sqlsc += string.Format(" nvarchar({0}) ",
                            column.MaxLength == -1 ? "max" : column.MaxLength.ToString());
                        break;
                }

                if (column.ColumnName == "Id")
                {
                    sqlsc += " PRIMARY KEY ";
                }
                else if (column.ColumnName.Contains("Id"))
                {
                    sqlsc += " FOREIGN KEY REFERENCES " + column.ColumnName.Substring(0, column.ColumnName.Length - 2) + "(Id) ";
                }
                if (!column.AllowDBNull)
                    sqlsc += " NOT NULL ";
                sqlsc += ",";
            }
            return sqlsc.Substring(0, sqlsc.Length - 1) + "\n)";
        }
    }
}
