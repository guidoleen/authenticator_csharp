using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OAuthDb
{
    public class MySqlDbManager : IDbDao<IObjectBrowser>
    {

        private MySqlConnection conn = new MySqlConnector().GetMySqlConnector();
        private MySqlCommand comm;
        private IObjectBrowser obj;
        Dictionary<String, Object> objList;

        public MySqlDbManager(IObjectBrowser obj)
        {
            this.obj = obj;
            this.objList = this.obj.GetObjectList();
        }

        private void createConnComm()
        {
            this.conn.Open();
            this.comm = this.conn.CreateCommand();
            this.comm.CommandType = System.Data.CommandType.Text;
        }

        public string delete(String id, String keyId)
        {
            this.createConnComm();
            this.comm.CommandText = "DELETE FROM " + this.createGetTypeFromObjString() + " WHERE " + keyId + "=@param1";

            this.comm.Parameters.Add(new MySqlParameter("@param1", id));

            this.comm.ExecuteNonQuery();
            this.conn.Close();

            return String.Format("Done delete {0}", id);
        }

        public string display(int id)
        {
            throw new NotImplementedException();
        }

        public string save(String keyId)
        {
            this.createConnComm();
            if (keyId == "")
                this.comm.CommandText = createInsertString();
            else
                this.comm.CommandText = createUpdateString(keyId);

            try
            {
                this.comm.ExecuteNonQuery();
            }
            catch(MySql.Data.MySqlClient.MySqlException ee)
            {
                return ee.ToString();
            }
            finally
            {
                this.conn.Close();
            }

            return String.Format("Done saved {0}", keyId);
        }

        // Insert into
        public String createInsertString()
        {
            int iCount = 0;
            String strParam = "";
            String strParamFlag = "";
            String strInsert = "INSERT INTO " + this.createGetTypeFromObjString() + "(";
            foreach (KeyValuePair<String, object> keyval in this.objList)
            {
                iCount++;
                strInsert += keyval.Key;
                strParamFlag = "@param" + iCount;
                strParam += strParamFlag;

                this.comm.Parameters.Add(new MySqlParameter(strParamFlag, keyval.Value));

                if (iCount < this.objList.Count)
                {
                    strInsert += ", ";
                    strParam += ", ";
                }

                strInsert += " ";
            }
            strInsert += ") VALUES (";
            strInsert += strParam;
            strInsert += ")";

            return strInsert;
        }

        // Update set
        public String createUpdateString(String KeyId)
        {
            int iCount = 0;
            String strParamFlag = "";
            String strInsert = "UPDATE " + this.createGetTypeFromObjString() + " SET ";
            foreach (KeyValuePair<String, object> keyval in this.objList)
            {
                iCount++;
                strInsert += keyval.Key + "=";
                strParamFlag = "@param" + iCount;
                strInsert += strParamFlag;

                this.comm.Parameters.Add(new MySqlParameter(strParamFlag, keyval.Value));

                if (iCount < this.objList.Count)
                {
                    strInsert += ", ";
                }
            }
            strInsert += " WHERE ";
            strInsert += KeyId;
            strInsert += "=" + this.obj.GetObjectId();

            this.comm.Parameters.Add(new MySqlParameter("@param"+(iCount+1), this.obj.GetObjectId()));

            return strInsert;
        }
        
        private String createGetTypeFromObjString()
        {
            String[] strResult = this.obj.GetType().ToString().ToLower().Split('.');
            return strResult[1];
        }

        // Not in use
        public Dictionary<string, object> GetObjectList()
        {
            throw new NotImplementedException();
        }
    }
}

///////
///  this.comm.CommandText = "INSERT INTO bericht (title, bericht) VALUES (@param3, @param4)";
//this.comm.Parameters.Add(new SQLiteParameter("@param3", _loc.getBerTitel()));
//this.comm.Parameters.Add(new SQLiteParameter("@param4", _loc.getBerText()));