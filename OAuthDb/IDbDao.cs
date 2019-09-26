using System;

namespace OAuthDb
{
    public interface IDbDao<IObjectBrowser>
    {
        String delete(String keyId);

        String display();

        String save(String keyId, Boolean forceUpdate);
    }
}