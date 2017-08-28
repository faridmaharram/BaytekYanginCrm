using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Crm_Project.Models;
using Microsoft.Web.Helpers;
using WebMatrix.WebData;

namespace Crm_Project.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string EMail , string Password)
        {
                if (!WebSecurity.Login(EMail, Password))
                {
                    return View("Login");
                }
                else return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterViewModel Model)
        {
            if (ModelState.IsValid)
            {
                    var token = WebSecurity.CreateUserAndAccount(Model.EMail, Model.Password,
                        new {@Name = Model.Name, @Surname = Model.Surname}, false);
                    Roles.AddUserToRole(Model.EMail, "Members"); //yeni uye olusdugunda onlar normal uye olurlarç. admin nece elave edilirç sayit ilk qurulanda admin 1 dene ozunu elave edir lazim gelse roller dege bir bolme acariq ama heleki ehtiyac yoxduçş he anladm nece modulumuz qalib? qaqa onsda gurupu zadi sen yazmisan men bidene link verdimş  esas siparis qalibş onuda hell elesek elede bisey qalmiyacaqş ama dediyim kimi onu gerek bidefe skypda danisaq.men sabah adama gosterecem bunu gorek ne deyir ama men deqiq bilmeliyem ki nece modulmu qalib hansilari elemisik hansilar problemlidi / Sector Group Account Home Cari bunlar deqiqe hazirdi o birsileri sen elemisen dige birsey dege bilmeremş Stokda tam hazirdiş Teklifde hazir saylir ama teklif siparis ile elaqelidi menceçş teklifde siparis ver butonu qoyacamş ona tiklayanda boyuk ehtimal o teklif siparis moduluna dusecek orda oz islemleri gedecekş men ele basa dusmusem duz basa dusmusen diyesen dediyim sayta baxdin ? yox qaqa baxa bilmemisemşş ora mutleq bax cunku orda bun senaryo var yaxsi qaqa baxacamş bidene bu githubu 
                    return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Login");
            
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return Redirect("/");
        }
    }
}