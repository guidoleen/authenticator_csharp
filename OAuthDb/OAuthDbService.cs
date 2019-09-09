using System;

namespace OAuthDb
{
    public class OAuthDbService
    {
        // Init App 
        // Initialize the application based on different roles and actions
        public String InitApp()
        {
            return createActionsInDB(3)[0].GetObjectId() + " - " + createActionsInDB(3)[0].GetObjectIdName(); ;
        }

        // Client login process
        // This process begins with login credentials > Create Jwt token > Store Unique key based on TimeStamp
        // Creates the token for further api calls
        public String LoginToCreateJwtToken(String strEmail, String strPwd)
        {
            String strJwtToken = "";
            MySqlDbUserDAO myssqlUserDao = new MySqlDbUserDAO();

            if(myssqlUserDao.IsValidUser(strEmail, strPwd))
            {
                // First get userId
                String UserId = myssqlUserDao.GetUser().GetObjectId();

                // Then create Unique key
                String strUniqueKey = new OAuthDbUtil().CreateTokenCredentialOnTimeStamp();

                // Store Unique key with userId in Db Token table > to Refer when making API connection
                String tokenDAO = new TokenDAO().StoreToken(Convert.ToInt32(UserId),
                    strUniqueKey,
                    "");
                    Console.Write(tokenDAO); // Console write > Is not nescessary

                // Create Jwt Token for usage
                TokenJWT tokenJwt = new TokenJWT(true, 
                                    new EncryptionDecryption(strUniqueKey).Encrypt(UserId) 
                                    );

                strJwtToken = tokenJwt.CreateTokenJWT(strUniqueKey);
            }
            return strJwtToken;
        }

        // Create the Objects for Action And Role
        private ActionDb[] createActionsInDB(int initStringLength)
        {
            int iActionStringLen = OAuthDbCONST.DB_ACTIONS.Length;
            int iActionLen = OAuthDbCONST.DB_ACTIONS.Length/initStringLength;
            int iActionTelr = 0;

            ActionDb[] actions = new ActionDb[iActionLen];
            
            for(int i = 0; i < iActionStringLen; i++)
            {
                if((i % iActionLen) == 0)
                {
                    actions[iActionTelr] = new ActionDb()
                    .SetAction(OAuthDbCONST.DB_ACTIONS[i])
                    .SetActionName(OAuthDbCONST.DB_ACTIONS[i + 1])
                    .SetActionDescription(OAuthDbCONST.DB_ACTIONS[i + 2]);
                }
            }

            return actions;
        }
    }
}
