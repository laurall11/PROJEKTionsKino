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

            //DbConnection.ConnectionString = "User Id=scott;Password=tiger;Data Source=oracle";
            DbConnection.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=infdb.technikum-wien.at)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=O10)));User Id=s20bwi4_wi18b092;Password=dbss20;";

            OracleCommand cmd = DbConnection.CreateCommand();

            DbConnection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Person";
            OracleDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                //var data = reader.GetFieldValue<string>(2);   spezifische column
                object[] values = new object[reader.FieldCount];
                var data = reader.GetValues(values);
                Console.WriteLine(data.ToString());
            }
        }
    }
}
