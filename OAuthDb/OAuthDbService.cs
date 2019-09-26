using System;

namespace OAuthDb
{
    public class OAuthDbService
    {
        // Init App 
        // Initialize the application based on different roles and actions
        public String InitApp()
        {
            String strInit = "";
            // Put the different roles and actions in db
            strInit += this.SetActionRoleInDBOnInit();
            strInit += this.SaveAdminRoles();

            return strInit;
        }

        // Client login process
        // This process begins with login credentials > Create Jwt token > Store Unique key based on TimeStamp
        // Creates the token for further api calls
        public String LoginToCreateJwtToken(String strEmail, String strPwd)
        {
            String strJwtToken = "";
            MySqlDbUserDAO myssqlUserDao = new MySqlDbUserDAO();

            if(myssqlUserDao.IsValidUser(strEmail, strPwd))
            {
                // First get userId
                String UserId = myssqlUserDao.GetUser().GetObjectId();

                // Then create Unique key
                String strUniqueKey = new OAuthDbUtil().CreateTokenCredentialOnTimeStamp();

                // Store Unique key with userId in Db Token table > to Refer when making API connection
                String tokenDAO = new TokenDAO().StoreToken(Convert.ToInt32(UserId),
                    strUniqueKey,
                    "");
                    Console.Write(tokenDAO); // Console write > Is not nescessary

                // Create Jwt Token for usage
                TokenJWT tokenJwt = new TokenJWT(
                                        UserId,
                                        strUniqueKey
                                    );

                strJwtToken = tokenJwt.CreateTokenJWT(strUniqueKey);
            }
            return strJwtToken;
        }

        // INSERT or UPDATE User
        // If no id is given the insert query will work, otherwise the 
        // Update query works.
        public String CreateOrUpdateUser(User usr)
        {
            MySqlDbManager msmmngr = new MySqlDbManager(usr);
            return msmmngr.save(usr.GetObjectId(), false);
        }

        // DELETE User
        public String DeleteUser(User usr)
        {
            MySqlDbManager msmmngr = new MySqlDbManager(usr);
            return msmmngr.delete(usr.GetObjectId());
        }

        // INSERT or UPDATE UserRole >> Force update is for updating existing user with new role
        public string CreateOrUpdateUserRole(UserRole usrRole, Boolean forceUpdate)
        {
            MySqlDbManager msmmngr = new MySqlDbManager(usrRole);
            return msmmngr.save(usrRole.GetObjectId(), forceUpdate);
        }

        // DELETE UserRole
        public String DeleteUserRole(UserRole usrRole)
        {
            MySqlDbManager msmmngr = new MySqlDbManager(usrRole);
            return msmmngr.delete(usrRole.GetObjectId());
        }

        // INIT Process 
        // for inserting roles and actions
        // Create the Objects for Action And Role
        private IObjectBrowser[] createIBrowserArrInDB(int initStringLength, String[] strConst, IObjectBrowser objB)
        {
            IObjectBrowser[] objBr = null;

            this.SetObjectBrowserOnType(initStringLength, out objBr, strConst, objB);

            return objBr;
        }

        // General function for creating object array > for storing in DB
        private void SetObjectBrowserOnType(int initStringLength, out IObjectBrowser[] objBr, String[] strConst, IObjectBrowser objB)
        {
            int iStringLen = strConst.Length;
            int iLen = strConst.Length / initStringLength;
            int iTelr = 0;
            objBr = new IObjectBrowser[iLen];
            String strObjType = objB.GetType().ToString().Split('.')[1];

            for (int i = 0; i < iStringLen; i++)
            {
                if ((i % initStringLength) == 0)
                {
                    if (strObjType.Equals("ActionDb"))
                        objBr[iTelr] = new ActionDb(
                                   strConst[i],
                                   strConst[i + 1],
                                   strConst[i + 2]
                               );
                        
                    if (strObjType.Equals("Role"))
                        objBr[iTelr] = new Role(
                                strConst[i],
                                strConst[i + 1],
                                strConst[i + 2]
                            );

                    iTelr++;
                }
            }
        }

        // 
        private String SetActionRoleInDBOnInit()
        {
            String strInit = "";
            // Create Actions from Constant vars
            strInit = new MysqlDbJoinDAO().SaveInObjects(
                                        createIBrowserArrInDB(
                                            3,
                                            OAuthDbCONST.DB_ACTIONS,
                                            new ActionDb()
                                            )
                                        );

            // Create Roles from Constant vars
            strInit += new MysqlDbJoinDAO().SaveInObjects(
                                        createIBrowserArrInDB(
                                            3,
                                            OAuthDbCONST.DB_ROLES,
                                            new Role()
                                            )
                                        );

            return strInit;
        }

        // TODO
        // Set admin actions in db > roleaction db
        // This is an admin setting for all the actions an admin should have
        private String SaveAdminRoles()
        {
            IDbDao<RoleAction> roleActionDAO = (RoleActionDAO)new RoleActionDAO()
            .SetActionsForRole(OAuthDbCONST.DB_ROLEACTIONS_ADMIN);

            return roleActionDAO.save(OAuthDbCONST.DB_ROLES[0],false);
        }

    }
}
