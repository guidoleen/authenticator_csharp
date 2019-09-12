using System;
using System.Collections.Generic;

namespace OAuthDb
{
    public class UserRole : IObjectBrowser
    {
        private int userId;
        private String roleName;

        public UserRole SetUserId(int userId)
        {
            this.userId = userId;
            return this;
        }
        public UserRole SetRoleName(String roleName)
        {
            this.roleName = roleName;
            return this;
        }

        private Dictionary<String, Object> UserRoleList = new Dictionary<String, Object>();

        public Dictionary<String, Object> GetObjectList()
        {
            UserRoleList.Add(OAuthDbCONST.DBCOLUMN_USRROLE_USRID, this.userId);
            UserRoleList.Add(OAuthDbCONST.DBCOLUMN_USRROLE_ROLEID, this.roleName);

            return this.UserRoleList;
        }

        public string GetObjectId()
        {
            return this.userId.ToString();
        }

        public string GetObjectIdName()
        {
            return OAuthDbCONST.DBCOLUMN_USRROLE_USRID;
        }
    }
}
