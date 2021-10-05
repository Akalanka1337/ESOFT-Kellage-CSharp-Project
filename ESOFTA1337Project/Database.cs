using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ESOFTA1337Project
{
    class Database
    {
        public MySqlConnection connection = new MySqlConnection("server=127.0.0.1;user=root;password=;persistsecurityinfo=True;port=3306;database=aka1337_temp;SslMode=none");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public MySqlConnection getConnection()
        {
            return connection;
        }

    }
}
