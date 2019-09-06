using System;

namespace OAuthDb
{
    public class OAuthDbService
    {
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

                // TODO 
                // Store Unique key with userId in Db Token table
                // strUniqueKey

                // Create Jwt Token for usage
                TokenJWT tokenJwt = new TokenJWT(true, 
                                    new EncryptionDecryption(strUniqueKey).Encrypt(UserId) 
                                    );

                strJwtToken = tokenJwt.CreateTokenJWT(strUniqueKey);
            }
            return strJwtToken;
        }
    }
}
