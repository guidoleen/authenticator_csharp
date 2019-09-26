using System;

namespace OAuthDb
{
    public class OAuthDbCONST
    {
        // DB INIT
        private static string DB_NAME = "oathdb";
        private static string DB_HOST = "localhost";
        private static string DB_USR = "root";
        private static string DB_PWD = "guido";

        public static String DB_MYSQLCONN = String.Format("Persist Security Info=False;database={0};server={1};port=3306;Connect Timeout = 30; user id={2}; pwd={3}",
                                            DB_NAME, DB_HOST, DB_USR, DB_PWD);
        public static int PWD_SALTLENGTH = 16;
        public static String PWD_SALTKEY = "dj75617asd21alq3";
        public static int DB_BRUTEFORCE = 3;

        // JWT INIT
        public static String JWT_ISS = "localhost:43001";
        public static String JWT_AUD = "localhost";
        public static int JWT_XTRA_DAY = 2;
        public static int JWT_XTRA_MINUTE = 30;
        public static bool JWT_SHOWROLES = false;

        // DB INIT
        public static String[] DB_ROLES =
        {
            "ADM", "Admin", "Admin role",
            "USR", "User Plain", "User Plain role",
            "PRO", "Project Manager", "Project Manager role"
        };

        public static String[] DB_ACTIONS =
        {
            "C", "Create", "Create action",
            "R", "Read", "Read action",
            "U", "Update", "Update action",
            "D", "Delete", "Delete action"
        };

        public static String[] DB_ROLEACTIONS_ADMIN = GetCodesFromArray(3, DB_ACTIONS);

        // DB COLUMNNAMES >> Mapping to DB
        public static string DBCOLUMN_RAN_ACTION = "fkatn_action";
        public static string DBCOLUMN_RAN_ROLE = "fkrle_role";

        public static string DBCOLUMN_USRROLE_USRID = "fkusr_id";
        public static string DBCOLUMN_USRROLE_ROLEID = "fkrle_role";

        public static string DBCOLUMN_TKN_TOKEN = "tkn_token";
        public static string DBCOLUMN_TKN_USRID = "usr_id";
        public static string DBCOLUMN_TKN_REVOKED = "tkn_revoked";
        public static string DBCOLUMN_TKN_DATE = "tkn_date";

        public static string DBCOLUMN_RLE_ROLENAME = "rle_name";
        public static string DBCOLUMN_RLE_ROLEDESCR = "rle_descr";
        public static string DBCOLUMN_RLE_ROLE = "rle_role";

        public static string DBCOLUMN_ATN_ACTIONNAME = "atn_name";
        public static string DBCOLUMN_ATN_ACTIONDESCR = "atn_descr";
        public static string DBCOLUMN_ATN_ACTION = "atn_action";

        public static string DBCOLUMN_USR_USERNAME = "usr_name";
        public static string DBCOLUMN_USR_PWD = "usr_pwd";
        public static string DBCOLUMN_USR_EMAIL = "usr_email";
        public static string DBCOLUMN_USR_ID = "id";

        // DB Messages
        public static string DB_MESS_DONESAVE = "Done saving";
        public static string DB_MESS_DONEMULTIOBJ = "Done saving";

        // Xtra functions for Constants
        private static String[] GetCodesFromArray(int iTelColumn, String[] strArr)
        {
            int index = 0;
            int iLen = strArr.Length;
            String[] strResult = new String[(int)iLen/iTelColumn];

            for (int i = 0; i <iLen; i++)
            {
                if((i % iTelColumn) == 0)
                {
                    strResult[index] = strArr[i];
                    index++;
                }
            }

            return strResult;
        }
    }
}
