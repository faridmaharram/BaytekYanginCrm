using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Crm_Project
{
    public class AccountConfig
    {
        public static void Init()
        {
            WebSecurity.InitializeDatabaseConnection("accountdb","Users", "Id", "EMail",true);

            if (!Roles.RoleExists("Administrators"))
                Roles.CreateRole("Administrators");
            if (!Roles.RoleExists("Members"))
                Roles.CreateRole("Members");

            if (!WebSecurity.UserExists("admin@baytek.com"))
            {
                WebSecurity.CreateUserAndAccount("admin@baytek.com", "admin", new { @Name = "adminname", @Surname = "adminsurname" }, false);
                Roles.AddUserToRole("admin@baytek.com", "Administrators");
            }
        }
    }
}