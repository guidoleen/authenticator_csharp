using System;

namespace OAuthDb
{
    public class UserRoleDAO : IDbDao<UserRole>
    {
        UserRole userRole;
        MySqlDbManager userRoleMngr; // IDbDao<IObjectBrowser>
        String userColumnName = OAuthDbCONST.DBCOLUMN_USRROLE_ROLEID;

        public UserRoleDAO(UserRole userRole)
        {
            this.userRole = userRole;
            this.userRoleMngr = new MySqlDbManager(this.userRole);
        }

        public string delete(string keyId)
        {
            throw new NotImplementedException();
        }

        public string display()
        {
            this.userRoleMngr.SetSelectQueryString(this.userColumnName);

            return this.userRoleMngr.display();
        }

        public string save(string keyId, Boolean forceUpdate)
        {
            throw new NotImplementedException();
            // return this.userRoleMngr.save(keyId);
        }
    }
}