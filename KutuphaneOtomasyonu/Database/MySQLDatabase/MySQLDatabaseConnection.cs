using MySql.Data.MySqlClient;
using System.Data;

namespace Database.MySQLDatabase
{
    internal class MySQLDatabaseConnection
    {
        MySqlConnection cn = new MySqlConnection("SERVER=127.0.0.1;DATABASE=Kutuphane;UID=root;PWD=1234");

        public MySqlConnection ConnectionOpen()
        {
            if (cn.State != ConnectionState.Open)
                cn.Open();
            return cn;
        }

        public MySqlConnection ConnectionClose()
        {
            if (cn.State != ConnectionState.Closed)
                cn.Close();
            return cn;
        }
    }
}
