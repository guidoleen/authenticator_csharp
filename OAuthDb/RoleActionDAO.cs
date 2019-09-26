using System;

namespace OAuthDb
{
    public class RoleActionDAO : IDbDao<RoleAction>
    {
        private String[] ActionsForRole;
        private RoleAction[] roleActions;

        public RoleActionDAO SetActionsForRole(String[] ActionsForRole)
        {
            this.ActionsForRole = ActionsForRole;
            return this;
        }

        // NOT in USE.....
        public string delete(string keyId)
        {
            throw new NotImplementedException();
        }

        public RoleAction[] GetRoleActions()
        {
            return this.roleActions;
        }

        public string display()
        {
            throw new NotImplementedException();
        }

        public string save(string keyId, Boolean forceUpdate)
        {
            if (this.ActionsForRole == null)
                return "";

            String strSaveResult = "";
            int iLen = this.ActionsForRole.Length;
            this.roleActions = new RoleAction[iLen];

            for (int i = 0; i < iLen; i++)
            {
                roleActions[i] = new RoleAction()
                    .SetActionName(this.ActionsForRole[i])
                    .SetRoleName(keyId);
            }
            return strSaveResult = saveInDb();
        }

        // Call the MysqlJoin class > with Transaction Command
        private String saveInDb()
        {
            MysqlDbJoinDAO mngrJoin = new MysqlDbJoinDAO();
            return mngrJoin.SaveInObjects( this.roleActions );
        }
    }
}
