using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Oracle.ManagedDataAccess.Client;
using PROJEKTionsKino_Frontend.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;

namespace PROJEKTionsKino_Frontend.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Ticket kaufen

        public RelayCommand TicketKaufenBtnClickedCmd { get; set; }
        public ObservableCollection<Film> Filme { get; set; }
        public Film SelectedFilm { get; set; }

        #endregion Ticket kaufen

        #region Kunde anlegen

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

        private ObservableCollection<Kunde> kunden;

        public ObservableCollection<Kunde> Kunden
        {
            get => kunden;
            set { kunden = value; }
        }

        #endregion Kunde anlegen

        public OracleConnection DbConnection = new OracleConnection
        {
            ConnectionString =
                "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=infdb.technikum-wien.at)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=O10)));User Id=s20bwi4_wi18b055;Password=dbss20;"
        };

        public MainViewModel()
        {
            Kunden = new ObservableCollection<Kunde>();
            Filme = new ObservableCollection<Film>();

            AddCustomerClickedCmd = new RelayCommand(
                () =>
                {
                    AddKunde();
                    GetKunden();
                });

            TicketKaufenBtnClickedCmd = new RelayCommand(
                () =>
                {
                    BuyTicket();
                });

            if (!IsInDesignMode)
            {
                GetKunden();
                GetFilme();
            }
        }

        private void BuyTicket()
        {
            throw new NotImplementedException();
        }

        private void AddKunde()
        {
            DbConnection.Open();
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

            DbConnection.Close();
        }

        public void GetKunden()
        {
            DbConnection.Open();
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
            }

            DbConnection.Close();
            Kunden = new ObservableCollection<Kunde>(Kunden.OrderBy(i => i.ID));
        }

        private void GetFilme()
        {
            DbConnection.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = DbConnection;

            cmd.CommandText = "p_view_programmdetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            OracleDataReader reader = cmd.ExecuteReader();
            object[] values;
            while (reader.Read())
            {
                values = new object[reader.FieldCount];
                reader.GetValues(values);
                bool exists = false;
                foreach (var film in Filme)
                {
                    if (film.FilmID == Convert.ToInt32(values[9]))
                    {
                        exists = true;
                    }
                }

                if (!exists)
                {
                    //progammbeginn, programmende, filmname, dauer, altersfreigabe, sitzplatzanzahl, beschreibung, genre
                    //regie, filmid, erscheinungsjahr, ratinganzahl, ratingsterne, saalid, programmid
                    Film tmp = new Film(Convert.ToInt32(values[9]), Convert.ToInt32(values[3]),
                        Convert.ToInt32(values[4]), Convert.ToInt32(values[10]), Convert.ToInt32(values[11]),
                        Convert.ToInt32(values[12]), (string)values[2],
                        (string)values[6], (string)values[7], (string)values[8]);
                    Filme.Add(tmp);
                }
            }

            DbConnection.Close();
            Filme = new ObservableCollection<Film>(Filme.OrderBy(i => i.FilmID));
        }
    }
}