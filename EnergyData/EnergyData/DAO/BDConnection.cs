using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EnergyData.DAO {
    class BDConnection {
        String server, database, user, pwd;
        MySqlConnection connection = null;

        public BDConnection(String server, String database, String user, String pwd) {
            this.database = database;
            this.pwd = pwd;
            this.user = user;
            this.server = server;
        }

        public MySqlConnection connect() {
            if(connection == null) {
                this.connection = new MySqlConnection("Server=" + server + ";Database=" + database + ";Uid=" + user + ";Pwd=" + pwd);
            } else {
                Console.WriteLine("Data: " + DateTime.Now + "Conexão já aberta ");
                throw new Exception("Conexão já aberta ");
            }
            return this.connection;
        }

    }
}
