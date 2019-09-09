using System;

namespace OAuthDb
{
    public class TokenDAO
    {
        // Store the token and use this method from the service
        public String StoreToken(int UserId, String CredentialToken, String strKeyId)
        {
            Token token = new Token()
               .SetDateToken()
               .SetRevoked(0)
               .SetUserId(UserId)
               .SetToken(CredentialToken);

            IDbDao<IObjectBrowser> tokenDao = new MySqlDbManager(token);

            return tokenDao.save(strKeyId);
        }
    }
}
