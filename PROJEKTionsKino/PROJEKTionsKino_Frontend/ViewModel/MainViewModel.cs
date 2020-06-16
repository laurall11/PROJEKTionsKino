using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.ObjectModel;
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
        public bool WantsVK { get; set; }

        private ObservableCollection<Kunde> kunden;

        public ObservableCollection<Kunde> Kunden
        {
            get { return kunden; }
            set { kunden = value; }
        }


        public RelayCommand AddCustomerClickedCmd { get; set; }

        public MainViewModel()
        {
            AddCustomerClickedCmd = new RelayCommand(
                () =>
                {
                    OracleCommand cmd = DbConnection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "exec p_create_kunde3 (vorname IN VARCHAR, nachname IN VARCHAR, strasse IN VARCHAR, hausnummer IN INT, postleitzahl IN INT, stadt IN VARCHAR, erstelldatum IN DATE, geburtstag IN DATE, kundenid OUT INT)";
                });

            if (!IsInDesignMode)
            {
                DbConnection = new OracleConnection
                {
                    ConnectionString =
                        "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=infdb.technikum-wien.at)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=O10)));User Id=s20bwi4_wi18b092;Password=dbss20;"
                };
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
}