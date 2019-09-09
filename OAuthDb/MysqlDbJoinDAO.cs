using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace OAuthDb
{
    /// <summary>
    /// TODO >> If nescessary
    /// </summary>
    public class MysqlDbJoinDAO
    {
        // Only for inserting to the database
        public String SaveInObjects(IObjectBrowser[] mnrgObj)
        {
            if (mnrgObj.Length == null) return "Create objects...";

            // Connection Obj
            MySqlDbManager mngrSave = new MySqlDbManager();
            mngrSave.GetConn().Open();

            MySqlCommand comm = mngrSave.GetConn().CreateCommand();

            // Transaction Obj from connection
            MySql.Data.MySqlClient.MySqlTransaction tr =
            mngrSave.GetConn().BeginTransaction();

            // Pass the tr to the Command
            comm.Transaction = tr;

            try
            {
                for (int i = 0; i < mnrgObj.Length; i++)
                {
                    comm.CommandType = CommandType.Text;
                    mngrSave.SetObject(mnrgObj[i]);
                    comm.CommandText = mngrSave.createInsertString(comm);

                    comm.ExecuteNonQuery();
                }
                tr.Commit();
            }
            catch (MySql.Data.MySqlClient.MySqlException ee)
            {
                tr.Rollback();
                return ee.ToString();
            }
            finally
            {
                mngrSave.GetConn().Close();
            }

            return "Done join connection";
        }
    }
}