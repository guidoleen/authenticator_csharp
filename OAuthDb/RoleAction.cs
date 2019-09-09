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
            RoleActionList.Add("fkatn_action", this.actionName);
            RoleActionList.Add("fkrle_role", this.roleName);
            // ActionList.Add("id", this.id);
            return this.RoleActionList;
        }

        public string GetObjectId()
        {
            return this.roleName;
        }

        public string GetObjectIdName()
        {
            return "fkrle_role";
        }
    }
}
