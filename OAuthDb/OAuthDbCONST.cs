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

        public static String[] DB_ROLEACTIONS_ADMIN =
        {
                "C",
                "R",
                "U",
                "D"
        };
    }
}
