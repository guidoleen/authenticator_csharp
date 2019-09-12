using System;

namespace OAuthDb
{
    // This class is only for displaying the actions in a role
    // based on the userId of a current user
    public class RoleDAO
    {
        // "SELECT actioncode FROM actiondb where roleid = ";
        public String displayActionsOrRole(int keyId)
        {
            String strDisplayActionsRoles = "";
            String strUserRole = new UserRoleDAO(new UserRole()
                .SetUserId(keyId)
                ).display();

            if (OAuthDbCONST.JWT_SHOWROLES)
            {
                MySqlDbManager dbmngr = new MySqlDbManager(new RoleAction().SetRoleName(strUserRole));
                dbmngr.SetSelectQueryString(OAuthDbCONST.DBCOLUMN_RAN_ACTION);

                strDisplayActionsRoles = dbmngr.display();
            }
            else
                strDisplayActionsRoles = strUserRole;

            return strDisplayActionsRoles;
        }
	}
}
