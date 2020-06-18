using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Oracle.ManagedDataAccess.Client;
using PROJEKTionsKino_Frontend.Model;
using System;
using System.Collections.Generic;
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

        public Film SelectedFilm
        {
            get => _selectedFilm;
            set
            {
                _selectedFilm = value;
                Vorstellungen = vDict[SelectedFilm.FilmID];
                RaisePropertyChanged("Vorstellungen");
            }
        }

        public Dictionary<int, ObservableCollection<Vorstellung>> vDict { get; set; }

        private ObservableCollection<Vorstellung> vorstellungen;

        public ObservableCollection<Vorstellung> Vorstellungen
        {
            get { return vorstellungen; }
            set { vorstellungen = value; }
        }

        private Vorstellung selectedVorstellung;

        public Vorstellung SelectedVorstellungen
        {
            get { return selectedVorstellung; }
            set
            {
                selectedVorstellung = value;
                VorstellungSelected = true;
                if (value != null)
                {
                    CheckSeats(value.VorstellungID);
                }
                TicketKaufenBtnClickedCmd.RaiseCanExecuteChanged();
            }
        }

        public bool VorstellungSelected { get; set; }

        private ObservableCollection<Sitzplatz> belegteSitzplaetze;

        public ObservableCollection<Sitzplatz> BelegteSitzplaetze
        {
            get { return belegteSitzplaetze; }
            set { belegteSitzplaetze = value; }
        }

        private ObservableCollection<Sitzplatz> freieSitzplaetze;

        public ObservableCollection<Sitzplatz> FreieSitzplaetze
        {
            get { return freieSitzplaetze; }
            set { freieSitzplaetze = value; }
        }

        public ObservableCollection<int> FreieSitzplatzID { get; set; }


        private int selectedSitzplatz;

        public int SelectedSitzplatz
        {
            get { return selectedSitzplatz; }
            set { selectedSitzplatz = value; }
        }

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

        public ObservableCollection<Saal> Saale { get; set; }

        public OracleConnection DbConnection = new OracleConnection
        {
            ConnectionString =
                "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=infdb.technikum-wien.at)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=O10)));User Id=s20bwi4_wi18b092;Password=dbss20;"
        };

        private Film _selectedFilm;

        public MainViewModel()
        {
            Kunden = new ObservableCollection<Kunde>();
            Filme = new ObservableCollection<Film>();
            Vorstellungen = new ObservableCollection<Vorstellung>();
            vDict = new Dictionary<int, ObservableCollection<Vorstellung>>();
            BelegteSitzplaetze = new ObservableCollection<Sitzplatz>();
            Saale = new ObservableCollection<Saal>();
            FreieSitzplatzID = new ObservableCollection<int>();

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
                }, () => { return VorstellungSelected; });

            if (!IsInDesignMode)
            {
                GetKunden();
                GetFilme();
            }
        }

        private void BuyTicket()
        {
            DbConnection.Open();

            OracleCommand buyTicketCmd = new OracleCommand("p_buy_ticket2", DbConnection);
            buyTicketCmd.CommandType = CommandType.StoredProcedure;
            buyTicketCmd.Parameters.Add("ticketID", OracleDbType.Int32).Direction = ParameterDirection.Output;
            buyTicketCmd.Parameters.Add("vorstellungsID", OracleDbType.Int32).Value = 1;

            buyTicketCmd.Parameters.Add("sitzplatzID", OracleDbType.Int32).Value = SelectedSitzplatz;
            buyTicketCmd.Parameters.Add("vorteilskartenID", OracleDbType.Int32).Value = 3;
            buyTicketCmd.Parameters.Add("ticketkategorie", OracleDbType.Varchar2).Value = "Normal";
            buyTicketCmd.Parameters.Add("ausstellungszeit", OracleDbType.Date).Value = DateTime.Now;
            buyTicketCmd.Parameters.Add("preis", OracleDbType.Decimal).Value = 7;

            buyTicketCmd.ExecuteNonQuery();

            DbConnection.Close();

            CheckSeats(SelectedVorstellungen.VorstellungID);

            //buyTicketCmd.Parameters.Add("sitzplatzID", OracleDbType.Int32).Value = tempVorstellung.si

//            CREATE OR REPLACE
//PROCEDURE p_buy_ticket
//(ticketID OUT INT, vorstellungsID IN INT, sitzplatzID IN INT, vorteilskartenID IN INT, ticketkategorie IN VARCHAR, ausstellungszeit IN TIMESTAMP, preis IN DECIMAL)
//            AS
//                id INT;
//            BEGIN
//                SELECT max(ticketid) + 1 INTO id FROM ticket;
//            INSERT INTO ticket(ticketid, vorstellungsid, sitzplatzid, vorteilskartenid, ticketkategorie, ausstellungszeit, preis) VALUES(id, vorstellungsid, sitzplatzid, vorteilskartenid, ticketkategorie, ausstellungszeit, preis);
//            ticketID:= id;
//            END;
        }

        private void CheckSeats(int VorstellungsID)
        {
            BelegteSitzplaetze.Clear();
            FreieSitzplatzID.Clear();
            DbConnection.Open();
            OracleCommand checkSeatsCmd = new OracleCommand("p_get_empty_seats", DbConnection);
            checkSeatsCmd.Parameters.Add("vorstellungs_id", OracleDbType.Int32).Value = VorstellungsID;
            checkSeatsCmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            checkSeatsCmd.CommandType = CommandType.StoredProcedure;

            checkSeatsCmd.ExecuteNonQuery();

            OracleDataReader reader = checkSeatsCmd.ExecuteReader();
            object[] values;
            while (reader.Read())
            {
                //sitzplatzid INT, sitzplatzkategorieid INT, saalid INT, sitzplatznr INT, reihe int
                values = new object[reader.FieldCount];
                reader.GetValues(values);
                Sitzplatz TempSitzplatz = new Sitzplatz(Convert.ToInt32(values[0]),
                    Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), Convert.ToInt32(values[3]));
                BelegteSitzplaetze.Add(TempSitzplatz);
            }

            DbConnection.Close();

            foreach (var saal in Saale)
            {
                if (saal.SaalID == SelectedVorstellungen.SaalID)
                {
                    foreach (var sitzplatz in saal.SitzplatzIDs)
                    {
                        bool exists = false;
                        foreach (var belegterSitzplatz in BelegteSitzplaetze)
                        {
                            if (sitzplatz == belegterSitzplatz.SitzplatzID)
                            {
                                exists = true;
                            }
                        }

                        if (!exists)
                        {
                            FreieSitzplatzID.Add(sitzplatz);
                        }
                    }
                }
            }
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
            OracleCommand cmd = new OracleCommand("p_view_kunde", DbConnection);
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
            OracleCommand cmd = new OracleCommand("p_view_programmdetails", DbConnection);
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
                    vDict[Convert.ToInt32(values[9])] = new ObservableCollection<Vorstellung>();
                    Vorstellung tmp2 = new Vorstellung((DateTime)values[0], (DateTime)values[1], (string)values[2], (string)values[6], Convert.ToInt32(values[13]), Convert.ToInt32(values[5]), Convert.ToInt32(values[15]));
                    vDict[Convert.ToInt32(values[9])].Add(tmp2);
                }
                else
                {
                    Vorstellung tmp2 = new Vorstellung((DateTime)values[0], (DateTime)values[1], (string)values[2], (string)values[6], Convert.ToInt32(values[13]), Convert.ToInt32(values[5]), Convert.ToInt32(values[15]));
                    vDict[Convert.ToInt32(values[9])].Add(tmp2);
                }

                //bool saalExists = false;
                //foreach (var saal in Saale)
                //{
                //    if (saal.SaalID == Convert.ToInt32(values[14]))
                //    {
                //        saalExists = true;
                //    }
                //}

                //if (!saalExists)
                //{
                //    Saale.Add(new Saal(Convert.ToInt32(values[14]), Convert.ToInt32(values[5])));
                //}
            }

            DbConnection.Close();
            Filme = new ObservableCollection<Film>(Filme.OrderBy(i => i.FilmID));
        }
    }
}