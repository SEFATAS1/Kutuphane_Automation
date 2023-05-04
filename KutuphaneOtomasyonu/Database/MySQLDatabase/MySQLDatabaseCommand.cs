using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Controls;

namespace Database.MySQLDatabase
{
    public class MySQLDatabaseCommand
    {
        MySQLDatabaseConnection MyConnection = new MySQLDatabaseConnection();

        public MySqlCommand MyCommand(string SqlQuery)
        {
            MySqlCommand cmd = new MySqlCommand(SqlQuery, MyConnection.ConnectionOpen());
            return cmd;
        }

        public bool MyReader(MySqlCommand cmd)
        {
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                MyConnection.ConnectionClose();
                return true;
            }
            else
            {
                dr.Close();
                MyConnection.ConnectionClose();
                return false;
            }
        }

        public MySqlDataAdapter MySelectCommand(string SqlQuery, DataGrid dataGrid)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new(SqlQuery, MyConnection.ConnectionOpen());
            da.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            return da;
        }
    }
}
