using System;
using System.Data;
using MySql.Data;

namespace OAuthDb
{
    public class MySqlConnector
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        MySql.Data.MySqlClient.MySqlCommand comm;
        String strConnection = OAuthDbCONST.DB_MYSQLCONN;

        public MySql.Data.MySqlClient.MySqlConnection GetMySqlConnector()
        {
            this.conn = new MySql.Data.MySqlClient.MySqlConnection();
            this.conn.ConnectionString = this.strConnection;
            return this.conn;
        }

        public MySql.Data.MySqlClient.MySqlCommand GetMySqlCommand(String strSql)
        {
            // this.conn.Open();

            this.comm = this.conn.CreateCommand();
            // this.comm.CommandText = strSql;
            return this.comm;
        }
    }
}
