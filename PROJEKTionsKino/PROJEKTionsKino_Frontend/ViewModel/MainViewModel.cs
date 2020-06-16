using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Oracle.ManagedDataAccess.Client;
using PROJEKTionsKino_Frontend.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

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

        public RelayCommand AddCustomerClickedCmd { get; set; }
        public bool canAdd { get; set; } = false;

        private ObservableCollection<Kunde> kunden;

        public ObservableCollection<Kunde> Kunden
        {
            get => kunden;
            set { kunden = value; RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            AddCustomerClickedCmd = new RelayCommand(
                () =>
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
                        }

                        DbConnection.Close();
                    }

                }, () => canAdd);

            if (!IsInDesignMode)
            {
                OpenDb();
            }
        }

        private bool OpenDb()
        {
            DbConnection = new OracleConnection
            {
                ConnectionString =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=infdb.technikum-wien.at)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=O10)));User Id=s20bwi4_wi18b055;Password=dbss20;"
            };
            DbConnection.Open();
            canAdd = true;
            return true;
        }
    }
}