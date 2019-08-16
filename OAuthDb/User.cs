using System;
using System.Collections.Generic;

namespace OAuthDb
{
    public class User : IObjectBrowser
    {
        private String username { get; set; }
        private int userid { get; set; }
        private String pwd { get; set; }
        private String email { get; set; }
        private Dictionary<String, Object> UserList = new Dictionary<String, Object>();

        public User SetUserName(String username)
        {
            this.username = username;
            return this;
        }
        public User SetUserId(int userid)
        {
            this.userid = userid;
            return this;
        }
        public User SetPwd(String pwd)
        {
            this.pwd = new UserUTIL().PasswordHash(pwd);
            return this;
        }
        public User SetEmail(String email)
        {
            this.email = email;
            return this;
        }

        override
        public String ToString()
        {
            String strOut = String.Format("UserName {0} Password {1} Email {2} UserId {3}",
                this.username,
                this.pwd,
                this.email,
                this.userid
                );
            return strOut; 
        }

        public Dictionary<String, Object> GetObjectList()
        {
            UserList.Add("usr_name", this.username);
            UserList.Add("usr_pwd", this.pwd);
            UserList.Add("usr_email", this.email);
            // UserList.Add("@id", this.userid);
            return this.UserList;
        }

        public string GetObjectId()
        {
            return this.userid.ToString();
        }
    }
}
