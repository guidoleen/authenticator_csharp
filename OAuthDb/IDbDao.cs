using System;

namespace OAuthDb
{
    public interface IDbDao<IObjectBrowser>
    {
        String delete(String id, String keyId);

        String display(int id);

        String save(String keyId);
    }
}