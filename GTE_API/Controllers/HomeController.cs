using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using GTE_API.Models;
using GTE_BL;
using GTE_BL.Entities;
using GTE_BL.Services;

namespace GTE_API.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
           // return RedirectToAction("TestQQSolution", new { phone="3525122075"});
            return View();
        }

        public ActionResult TestQQSolution()
        {
            var uri = Request.Url;
            string phoneNum = Request.QueryString["phone"];
            ViewBag.Phone = phoneNum;
            Contact c;


            try
            {
                // strip the +1 from the phone number if they are there
                if (phoneNum.Contains("+"))
                    phoneNum = phoneNum.Remove(0, 1);

                if (phoneNum.Substring(0, 1) == "1")
                    phoneNum = phoneNum.Remove(0, 1);

            } 
            catch { }

            //check for the cookie
            HttpCookie cookie = Request.Cookies["GTESettings"];
            if (cookie != null)
            {

                //read the cookie in
                QQService service = new QQService();
                string token = HttpUtility.UrlDecode(cookie.Value.ToString().Remove(0, 12));
                service.AuthorizationToken = token.FromJson<AuthorizationState>();
                int entityId = service.SearchByPhoneNumber(phoneNum);
                if(entityId == -1)
                {
                    return Redirect("https://app.qqcatalyst.com/Contacts/CreateContact/Create?mode=basic");
                }
                else
                    return Redirect(string.Format("https://app.qqcatalyst.com/Contacts/Customer/Details/{0}", entityId.ToString()));
            }
            else
            {
                return View(new QQLogin());
            }          

        }

        [HttpPost]
        public ActionResult Login(QQLogin login)
        {
            //login.UserName = "asanfilippo28@gmail.com";
            //login.Password = "Br11256188";

            //set the users cookie
            HttpCookie cookie = new HttpCookie("GTESettings");

            QQService service = new QQService(login.UserName, login.Password);
            int entityId = service.SearchByPhoneNumber(login.SearchValue);

            cookie["GTESettings"] = service.AuthorizationToken.ToJson();
            cookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(cookie);

            return Redirect(string.Format("https://app.qqcatalyst.com/Contacts/Customer/Details/{0}", entityId));
        }

        [Route("Registration")]
        public ActionResult Registration()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Registration r)
        {
            if(ModelState.IsValid)  
            {
                try
                {
                    r.ApiProvider = APIProvider.QQSOLUTION;
                    r.Save();                    
                }
                catch { return PartialView("Error"); }
            }

            ViewBag.API = "QQSolutionAPI";
            ViewBag.CompanyName = r.CompanyName;  
            return PartialView("RegistrationSuccess");
        }

        [Route("VelocifyRegistration")]
        public ActionResult VelocifyRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VelocifyRegistration(Registration r)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    r.ApiProvider = APIProvider.VELOCIFY;
                    r.Save();
                }
                catch { return PartialView("Error"); }
            }
            ViewBag.API = "VelocifyAPI";
            ViewBag.CompanyName = r.CompanyName;
            return PartialView("RegistrationSuccess");
        }
    }
}