using System;
using System.Collections.Generic;

namespace OAuthDb
{
    public class ActionDb : IObjectBrowser
    {
        private String action { get; set; }
        private int id { get; set; }
        private String actionName { get; set; }
        private String actionDescription { get; set; }
        private Dictionary<String, Object> ActionList = new Dictionary<String, Object>();

        // Constructor
        public ActionDb()
        {
        }

        public ActionDb(String action, String actionName, String actionDescription)
        {
            this.action = action;
            this.actionName = actionName;
            this.actionDescription = actionDescription;
        }

        public ActionDb SetAction(String action)
        {
            this.action = action;
            return this;
        }
        public ActionDb SetActionId(int id)
        {
            this.id = id;
            return this;
        }
        public ActionDb SetActionName(String actionName)
        {
            this.actionName = actionName;
            return this;
        }
        public ActionDb SetActionDescription(String actionDescription)
        {
            this.actionDescription = actionDescription;
            return this;
        }

        override
        public String ToString()
        {
            String strOut = String.Format("Action {0} Id {1} ActionName {2} ActionDescription {3}",
                this.action,
                this.id,
                this.actionName,
                this.actionDescription
                );
            return strOut;
        }

        public Dictionary<String, Object> GetObjectList()
        {
            ActionList.Add(OAuthDbCONST.DBCOLUMN_ATN_ACTIONNAME, this.actionName);
            ActionList.Add(OAuthDbCONST.DBCOLUMN_ATN_ACTIONDESCR, this.actionDescription);
            ActionList.Add(OAuthDbCONST.DBCOLUMN_ATN_ACTION, this.action);
            // ActionList.Add("id", this.id);
            return this.ActionList;
        }

        public string GetObjectId()
        {
            return this.action;
        }

        public string GetObjectIdName()
        {
            return OAuthDbCONST.DBCOLUMN_ATN_ACTION;
        }
    }
}
