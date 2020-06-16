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
        public object test { get; set; }

        public string Vorname { get; set; } = "TotallyNotLaura";
        public string Nachname { get; set; } = "Legat";
        public string Strasse { get; set; } = "SomeStreet";
        public string Stadt { get; set; } = "SomeCity";
        public int Hausnr { get; set; } = 23;
        public int PLZ { get; set; } = 1190;




        public OracleConnection DbConnection { get; set; }

        //public string Vorname { get; set; }
        //public string Nachname { get; set; }
        //public string Strasse { get; set; }
        //public string Stadt { get; set; }
        //public int Hausnr { get; set; }
        //public int PLZ { get; set; }
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
            Kunden = new ObservableCollection<Kunde>();
            AddCustomerClickedCmd = new RelayCommand(
                () =>
                {
                    Kunde TempKunde = new Kunde(Vorname, Nachname, Strasse, Hausnr, PLZ, Stadt, Geburtstag, WantsVK);

                    OracleCommand addCustomerCmd = new OracleCommand("p_create_kunde7", DbConnection);
                    addCustomerCmd.CommandType = CommandType.StoredProcedure;
                    addCustomerCmd.Parameters.Add("vorname", OracleDbType.Varchar2).Value = TempKunde.Vorname;
                    addCustomerCmd.Parameters.Add("nachname", OracleDbType.Varchar2).Value = TempKunde.Nachname;
                    addCustomerCmd.Parameters.Add("strasse", OracleDbType.Varchar2).Value = TempKunde.Straﬂe;
                    addCustomerCmd.Parameters.Add("hausnummer", OracleDbType.Int32).Value = Convert.ToInt32(TempKunde.HausNr);
                    addCustomerCmd.Parameters.Add("postleitzahl", OracleDbType.Int32).Value = Convert.ToInt32(TempKunde.PLZ);
                    addCustomerCmd.Parameters.Add("stadt", OracleDbType.Varchar2).Value = TempKunde.Ort;
                    addCustomerCmd.Parameters.Add("erstelldatum", OracleDbType.Date).Value = TempKunde.Erstelldatum;
                    addCustomerCmd.Parameters.Add("geburtstag", OracleDbType.Date).Value = TempKunde.Geburtsdatum;

                    addCustomerCmd.ExecuteNonQuery();

                    GetKunden();

                    
                }, () => canAdd);

            if (!IsInDesignMode)
            {
                OpenDb();

                GetKunden();
            }
        }

        private void GetKunden()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = DbConnection;

            cmd.CommandText = "p_view_kunde";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            OracleDataReader reader = cmd.ExecuteReader();
            object[] values;
            while (reader.Read())
            {
                //ID, Vorname, Nachname, Straﬂe, Hausnummer, Postleitzahl, Ort, Geburtsdatum, Erstelldatum
                values = new object[reader.FieldCount];
                reader.GetValues(values);
                Kunde tmp = new Kunde(Convert.ToInt32(values[0]), (string)values[1], (string)values[2], (string)values[3], Convert.ToInt32(values[4]), Convert.ToInt32(values[5]), (string)values[6], (DateTime)values[7], (DateTime)values[8]);
                Kunden.Add(tmp);
                var test = 4;
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