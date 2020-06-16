using GalaSoft.MvvmLight;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace PROJEKTionsKino_Frontend.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public OracleConnection DbConnection { get; set; }

        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Strasse { get; set; }
        public string Stadt { get; set; }
        public int Hausnr { get; set; }
        public int PLZ { get; set; }
        public DateTime Erstelldatum { get; set; }
        public DateTime Geburtstag { get; set; }

        public MainViewModel()
        {
            DbConnection = new OracleConnection();
            DbConnection.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=infdb.technikum-wien.at)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=O10)));User Id=s20bwi4_wi18b092;Password=dbss20;";
            DbConnection.Open();

            OracleCommand cmd = DbConnection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Person";

            OracleDataReader reader = cmd.ExecuteReader();
            object[] values;
            while (reader.Read())
            {
                values = new object[reader.FieldCount];
                var row = reader.GetValues(values);
            }
        }
    }
}