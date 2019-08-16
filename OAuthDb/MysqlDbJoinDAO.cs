using System;

namespace OAuthDb
{
    /// <summary>
    /// TODO >> If nescessary
    /// </summary>
    public class MysqlDbJoinDAO
    {
        public String SaveRoleAction(IObjectBrowser mnrg1, IObjectBrowser mngr2)
        {

            MySqlDbManager mngrRole = new MySqlDbManager(mnrg1);
            MySqlDbManager mngrAction = new MySqlDbManager(mngr2);


            return String.Format("Done saved");
        }
    }
}

// Insert into Transaction
//public String InsertDbTransaction(String[] strSqlQuery, Object[] paramValues)
//{
//    String strMessage = "";
//    MySql.Data.MySqlClient.MySqlTransaction tr = this.conn.BeginTransaction();
//    this.comm.Transaction = tr;

//    try
//    {
//        for (int i = 0; i < strSqlQuery.Length; i++)
//        {
//            this.comm.CommandType = CommandType.Text;
//            this.comm.CommandText = strSqlQuery[i];

//            for (int j = 0; j < paramValues.Length; j++)
//                // this.comm.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter(paramValues));
//                this.comm.ExecuteNonQuery();
//        }
//        tr.Commit();
//    }
//    catch (MySql.Data.MySqlClient.MySqlException ee)
//    {
//        tr.Rollback();
//        strMessage = ee.ToString();
//    }
//    finally
//    {
//        this.conn.Close();
//    }

//    return strMessage;
//}