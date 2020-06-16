using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbtest2
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConnection DbConnection = new OracleConnection();
            DbConnection.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=infdb.technikum-wien.at)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=O10)));User Id=s20bwi4_wi18b055;Password=dbss20;";
            DbConnection.Open();

            OracleCommand cmd = DbConnection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT f_view_one FROM DUAL";
            //var value2 = cmd.ExecuteNonQuery();

            OracleDataReader reader = cmd.ExecuteReader();
            object[] values;
            while (reader.Read())
            {
                values = new object[reader.FieldCount];
                var row = reader.GetValues(values);
            }

            var length = 5;
        }
    }
}
