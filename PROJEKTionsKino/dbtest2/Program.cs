using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.OracleClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Oracle.ManagedDataAccess.Types;

namespace dbtest2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (OracleConnection DbConnection = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=infdb.technikum-wien.at)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=O10)));User Id=s20bwi4_wi18b058;Password=dbss20;"))
            {

                //DbConnection.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=infdb.technikum-wien.at)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=O10)));User Id=s20bwi4_wi18b055;Password=dbss20;";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = DbConnection;

                cmd.CommandText = "counter";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                DbConnection.Open();
                cmd.ExecuteNonQuery();

                OracleDataReader reader = cmd.ExecuteReader();
                object[] values;
                while (reader.Read())
                {
                    values = new object[reader.FieldCount];
                    reader.GetValues(values);
                }

                int length = 5;
            }
        }
    }
}
