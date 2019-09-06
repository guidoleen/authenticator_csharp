using MySql.Data.MySqlClient;
using System;

namespace OAuthDb
{
    public class MySqlDbUserDAO
    {
        private MySqlDbManager dbmngr;
        private static int iBruteforce = 0;
        private User usr;

        public User GetUser()
        {
            return this.usr;
        }

        public bool IsValidUser(String email, String pwd)
        {
            this.usr = new User()
                .SetEmail(email)
                .SetPwd(pwd);

            this.dbmngr = new MySqlDbManager(usr);

            this.dbmngr.createConnComm();
            this.dbmngr.GetComm().CommandText = this.createSelectUserString(usr);

            try
            {
                MySqlDataReader rdr = this.dbmngr.GetComm().ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows && iBruteforce <= OAuthDbCONST.DB_BRUTEFORCE)
                    {
                        this.usr.SetUserId( (int)rdr.GetValue(0)); // Get the user id from the sql query
                        return true;
                    }
                    else
                        iBruteforce++;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ee)
            {
                iBruteforce++;

                return false;
            }
            finally
            {
                this.dbmngr.GetConn().Close();
            }

            return false;
        }

        // Create Sql User String
        private String createSelectUserString(User usr)
        {
            String strParam = "@param";
            this.dbmngr.GetComm().Parameters.Add(new MySqlParameter(strParam + "1", usr.GetEmail()));
            this.dbmngr.GetComm().Parameters.Add(new MySqlParameter(strParam + "2", usr.GetPwd()));

            String strSelect = "SELECT id FROM ";
            strSelect += " user";
            strSelect += " WHERE";
            strSelect += " usr_email = " + strParam + "1";
            strSelect += " AND";
            strSelect += " usr_pwd = " + strParam + "2";

            return strSelect;
        }
    }
}
