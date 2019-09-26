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
        private String strSelectQuery = "*";
        Dictionary<String, Object> objList;

        // Get Conn
        public MySqlConnection GetConn()
        {
            return this.conn;
        }
        // Get Comm
        public MySqlCommand GetComm()
        {
            return this.comm;
        }

        // Set Object when empty constructor
        public void SetObject(IObjectBrowser _obj)
        {
            this.obj = _obj;
            this.objList = this.obj.GetObjectList();
        }

        // Constructor Plain
        public MySqlDbManager()
        {
        }

        // constructor with param Objeect
        public MySqlDbManager(IObjectBrowser obj)
        {
            this.obj = obj;
            this.objList = this.obj.GetObjectList();
        }

        public void createConnComm()
        {
            this.conn.Open();
            this.comm = this.conn.CreateCommand();
            this.comm.CommandType = System.Data.CommandType.Text;
        }

        public string delete(String keyId)
        {
            this.createConnComm();
            this.comm.CommandText = "DELETE FROM " + this.createGetTypeFromObjString() + " WHERE " + this.obj.GetObjectIdName() + "=@param1";

            this.comm.Parameters.Add(new MySqlParameter("@param1", keyId));

            this.comm.ExecuteNonQuery();
            this.conn.Close();

            return String.Format("Done delete {0}", keyId);
        }

        public string display()
        {
            String strDisplay = "";
            this.createConnComm();
            int index = 0;

            this.comm.CommandText = this.createSelectString();

            try
            {
                MySqlDataReader rdr = this.comm.ExecuteReader();

                while(rdr.Read())
                {
                    strDisplay += rdr.GetValue(0);
                    index++;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ee)
            {
                return ee.ToString();
            }
            finally
            {
                this.conn.Close();
            }

            return strDisplay;
        }

        public string save(String keyId, Boolean forceUpdate)
        {
            this.createConnComm();
            
            if (keyId == "" || forceUpdate != true)
                this.comm.CommandText = createInsertString(null);
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

            return String.Format(OAuthDbCONST.DB_MESS_DONESAVE + " {0}", keyId);
        }

        // Insert into
        private int iCount = 0;
        public String createInsertString(MySqlCommand commObj)
        {
            int iCountFlag = 0;
            String strParam = "";
            String strParamFlag = "";
            String strInsert = "INSERT INTO " + this.createGetTypeFromObjString() + "(";
            foreach (KeyValuePair<String, object> keyval in this.objList)
            {
                iCount++;
                iCountFlag++;
                strInsert += keyval.Key;
                strParamFlag = "@param" + iCount;
                strParam += strParamFlag;

                if(this.comm == null)
                    commObj.Parameters.Add(new MySqlParameter(strParamFlag, keyval.Value));
                else
                    this.comm.Parameters.Add(new MySqlParameter(strParamFlag, keyval.Value));

                if (iCountFlag < this.objList.Count)
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
        private String createUpdateString(String KeyId)
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
            strInsert += this.obj.GetObjectIdName();
            strInsert += "=" + KeyId;

            this.comm.Parameters.Add(new MySqlParameter("@param"+(iCount+1), this.obj.GetObjectId()));
            return strInsert;
        }

        private String createSelectString()
        {
            int iCount = 0;
            String strParamFlag = "";

            String strSelect = "SELECT " + this.strSelectQuery + " FROM ";
            strSelect += createGetTypeFromObjString();

            if(this.obj.GetObjectId() != "" )
            {
                iCount++;
                strParamFlag = "@param" + iCount;
                this.comm.Parameters.Add(new MySqlParameter(strParamFlag, this.obj.GetObjectId()));

                strSelect += " WHERE ";
                strSelect += this.obj.GetObjectIdName();
                strSelect += " = ";
                strSelect += strParamFlag;
            }
            return strSelect;
        }

        // Set select query with the unique column name
        public void SetSelectQueryString(String strKeyName)
        {
            this.strSelectQuery = strKeyName;
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