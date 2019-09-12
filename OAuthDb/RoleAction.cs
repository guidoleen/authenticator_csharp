using System;
using System.Collections.Generic;

namespace OAuthDb
{
    public class RoleAction : IObjectBrowser
    {
        private String actionName;
        private String roleName;

        public RoleAction SetActionName(String actionName)
        {
            this.actionName = actionName;
            return this;
        }
        public RoleAction SetRoleName(String roleName)
        {
            this.roleName = roleName;
            return this;
        }

        private Dictionary<String, Object> RoleActionList = new Dictionary<String, Object>();

        public Dictionary<String, Object> GetObjectList()
        {
            RoleActionList.Add(OAuthDbCONST.DBCOLUMN_RAN_ACTION, this.actionName);
            RoleActionList.Add(OAuthDbCONST.DBCOLUMN_RAN_ROLE, this.roleName);
            // ActionList.Add("id", this.id);
            return this.RoleActionList;
        }

        public string GetObjectId()
        {
            return this.roleName;
        }

        public string GetObjectIdName()
        {
            return OAuthDbCONST.DBCOLUMN_RAN_ROLE;
        }
    }
}
