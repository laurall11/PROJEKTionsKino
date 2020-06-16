using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
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

        public void test()
        {
            string ConnectionString = "Data Source=otndemo;User ID=scott;Password=tiger;Unicode=True";

            OracleConnection conn = new OracleConnection(ConnectionString);

            OracleCommand cmd = conn.CreateCommand();

            OracleString os = new OracleString();



            try

            {

                conn.Open();



                try

                {

                    cmd.CommandText = "begin dbms_output.enable; end;";
                    cmd.ExecuteNonQuery();

                }

                catch (OracleException ex)

                {

                    MessageBox.Show(ex.Message, "Fehler");

                }

                cmd.CommandText = this.textBoxInhalt.Text; // the PL/SQL-Block, read from file an put into a multiline textbox

                cmd.CommandText = cmd.CommandText.Replace('\r', ' ');

                cmd.CommandType = CommandType.Text;

                try

                {

                    cmd.ExecuteOracleNonQuery(out os);



                    cmd.CommandText = "begin dbms_output.get_line(:1,:2); end;";

                    OracleParameter p1 = new OracleParameter("1", OracleType.VarChar);

                    p1.Direction = ParameterDirection.Output;

                    p1.Size = 255;

                    OracleParameter p2 = new OracleParameter("2", OracleType.Number);

                    p2.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(p1);

                    cmd.Parameters.Add(p2);

                    this.textBoxDBMSOutput.Text = String.Empty;

                    try

                    {

                        cmd.ExecuteOracleNonQuery(out os);

                        while (p2.Value.ToString() == "0")

                        {

                            this.textBoxDBMSOutput.Text = this.textBoxDBMSOutput.Text + p1.Value.ToString() + "\r\n"; //put it into an multiline textbox

                            cmd.ExecuteOracleNonQuery(out os);

                        }

                    }

                    catch (OracleException ex)

                    {

                        MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }

                catch (OracleException ex)

                {

                    MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                finally

                {

                    conn.Close();

                }

            }

            catch (OracleException ex)

            {

                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
