using System;

namespace OAuthDb
{
    public class MySqlDbDao
    {
        public String saveObject(IObjectBrowser obj, String objKey)
        {
            MySqlDbManager dbmngr = new MySqlDbManager(obj);
            return dbmngr.save(objKey);
        }

        public String deleteObject(String id, String strKey, IObjectBrowser obj)
        {
            MySqlDbManager dbmngr = new MySqlDbManager(obj);
            return dbmngr.delete(id, strKey);
        }
    }
}