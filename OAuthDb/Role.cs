using System;
using System.Collections.Generic;

namespace OAuthDb
{
    public class Role : IObjectBrowser
    {
        private String role { get; set; }
        private int id { get; set; }
        private String roleName { get; set; }
        private String roleDescription { get; set; }
        private Dictionary<String, Object> RoleList = new Dictionary<String, Object>();

        // Constructor
        public Role()
        {
        }

        public Role(String role, String roleName, String roleDescription)
        {
            this.role = role;
            this.roleName = roleName;
            this.roleDescription = roleDescription;
        }

        public Role SetRole(String role)
        {
            this.role = role;
            return this;
        }
        public Role SetRoleId(int id)
        {
            this.id = id;
            return this;
        }
        public Role SetRoleName(String roleName)
        {
            this.roleName = roleName;
            return this;
        }
        public Role SetRoleDescription(String roleDescription)
        {
            this.roleDescription = roleDescription;
            return this;
        }

        override
        public String ToString()
        {
            String strOut = String.Format("Role {0} Id {1} RoleName {2} RoleDescription {3}",
                this.role,
                this.id,
                this.roleName,
                this.roleDescription
                );
            return strOut;
        }

        public Dictionary<String, Object> GetObjectList()
        {
            RoleList.Add(OAuthDbCONST.DBCOLUMN_RLE_ROLENAME, this.roleName);
            RoleList.Add(OAuthDbCONST.DBCOLUMN_RLE_ROLEDESCR, this.roleDescription);
            RoleList.Add(OAuthDbCONST.DBCOLUMN_RLE_ROLE, this.role);
            // RoleList.Add("id", this.id);
            return this.RoleList;
        }

        public string GetObjectId()
        {
            return this.role;
        }

        public string GetObjectIdName()
        {
            return OAuthDbCONST.DBCOLUMN_RLE_ROLE";
        }
    }
}
