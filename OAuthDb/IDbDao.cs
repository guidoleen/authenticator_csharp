using System;

namespace OAuthDb
{
    public interface IDbDao<IObjectBrowser>
    {
        String delete(String id, String keyId);

        String display();

        String save(String keyId);
    }
}