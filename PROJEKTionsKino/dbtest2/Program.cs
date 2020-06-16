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
