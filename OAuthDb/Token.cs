﻿using System;
using System.Collections.Generic;

namespace OAuthDb
{
    public class Token : IObjectBrowser
    {
        private String token { get; set; }
        private int userid { get; set; }
        private int revoked { get; set; }
        private DateTime datetoken { get; set; }
        private Dictionary<String, Object> TokenList = new Dictionary<String, Object>();

        public Token SetToken(String token)
        {
            this.token = token; // new OAuthDbUtil().CreateTokenCredentialOnTimeStamp();
            return this;
        }
        public Token SetUserId(int id)
        {
            this.userid = id;
            return this;
        }
        public Token SetRevoked(int revoked)
        {
            this.revoked = revoked;
            return this;
        }
        public Token SetDateToken()
        {
            this.datetoken = new OAuthDbUtil().CreateDateTimeNow();
            return this;
        }

        override
        public String ToString()
        {
            String strOut = String.Format("Token {0} UsrId {1} Revoked {2} DateTime {3}",
                this.token,
                this.userid,
                this.revoked,
                this.datetoken
                );
            return strOut;
        }

        public Dictionary<String, Object> GetObjectList()
        {
            TokenList.Add(OAuthDbCONST.DBCOLUMN_TKN_TOKEN, this.token);
            TokenList.Add(OAuthDbCONST.DBCOLUMN_TKN_USRID, this.userid);
            TokenList.Add(OAuthDbCONST.DBCOLUMN_TKN_REVOKED, this.revoked);
            TokenList.Add(OAuthDbCONST.DBCOLUMN_TKN_DATE, this.datetoken);
            // ActionList.Add("id", this.id);
            return this.TokenList;
        }

        public string GetObjectId()
        {
            return this.userid.ToString();
        }

        public string GetObjectIdName()
        {
            return OAuthDbCONST.DBCOLUMN_TKN_TOKEN;
        }
    }
}
