using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Text;
using GTE_BL;
using GTE_BL.Entities;
using GTE_BL.Services;

namespace GTE_API.App_Code
{
    public class QQSolutionAPIController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string companyName, string phoneNum)
        {
            //HttpResponseMessage
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.SeeOther);
            var cookie = Request.Headers.GetCookies("GTESettings").FirstOrDefault();
            Contact c;
            int entityId = -1;

            try
            {
                // strip the +1 from the phone number if they are there
                if (phoneNum.Contains("+"))
                    phoneNum = phoneNum.Remove(0, 1);

                if (phoneNum.Substring(0, 1) == "1")
                    phoneNum = phoneNum.Remove(0, 1);

            }
            catch { }

            // get the company login from the companyName parameter
            
            Registration r = new Registration();
            try
            {
                r.GetBy(companyName,APIProvider.QQSOLUTION.ToString());
            }
            catch
            {
                HttpError e = new HttpError(string.Format("Company {0} not found", companyName));
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e);
            }


            if (cookie != null)
            {
                //read the cookie in
                QQService service = new QQService();
                string token = HttpUtility.UrlDecode(cookie["GTESettings"].Values.ToString().Remove(0, 12));
                service.AuthorizationToken = token.FromJson<AuthorizationState>();
                entityId = service.SearchByPhoneNumber(phoneNum);
            }
            else
            {
                QQService service = new QQService(r.APIUserName, r.APIPassword);
                entityId = service.SearchByPhoneNumber(phoneNum);
                
                CookieHeaderValue newCookie = new CookieHeaderValue("GTESetting", service.AuthorizationToken.ToJson());
                newCookie.Expires = DateTime.Now.AddMonths(1);
                response.Headers.AddCookies(new CookieHeaderValue[] { newCookie });
            }

            if(entityId == -1)
            {
                response.Headers.Location = new Uri("https://app.qqcatalyst.com/Contacts/CreateContact/Create?mode=basic");
            }
            else
                response.Headers.Location = new Uri(string.Format("https://app.qqcatalyst.com/Contacts/Customer/Details/{0}", entityId.ToString()));

            return response;
        }


        //[HttpGet]     
        //[Route("api/QQSolutionAPI/{companyName/{phoneNum}")]
        //public HttpResponseMessage GetContactByPhone(string companyName, string phoneNum)
        //{
        //    //HttpResponseMessage
        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.SeeOther);
        //    var cookie = Request.Headers.GetCookies("GTESettings").FirstOrDefault();
        //    Contact c;

        //    try
        //    {
        //        // strip the +1 from the phone number if they are there
        //        if (phoneNum.Contains("+"))
        //            phoneNum = phoneNum.Remove(0, 1);

        //        if (phoneNum.Substring(0, 1) == "1")
        //            phoneNum = phoneNum.Remove(0, 1);

        //    }
        //    catch { }
            
        //    if (cookie != null)
        //    {
        //        //read the cookie in
        //        QQService service = new QQService();
        //        string token = HttpUtility.UrlDecode(cookie["GTESettings"].Values.ToString().Remove(0, 12));
        //        service.AuthorizationToken = token.FromJson<AuthorizationState>();
        //        c = service.SearchByPhoneNumber(phoneNum);                      
        //    }
        //    else
        //    {
        //        string userName = "asanfilippo28@gmail.com";
        //        string password = "Br11256188";

        //        QQService service = new QQService(userName, password);
        //        c = service.SearchByPhoneNumber(phoneNum);

        //        //cookie["GTESettings"] = service.AuthorizationToken.ToJson();
        //        CookieHeaderValue newCookie = new CookieHeaderValue("GTESetting", service.AuthorizationToken.ToJson());
        //        newCookie.Expires= DateTime.Now.AddMonths(1);
        //        response.Headers.AddCookies(new CookieHeaderValue[] { newCookie });            
        //    }
         
        //    response.Headers.Location = new Uri(string.Format("https://app.qqcatalyst.com/Contacts/Customer/Details/{0}",c.EntityID));
        //    return response;
        //}

        //public HttpResponseMessage Get()
        //{
        //    var response = Request.CreateResponse(HttpStatusCode.Moved);
        //    response.Headers.Location = new Uri("http://www.google.com");
        //    return response;
        //}

        //public HttpResponseMessage Get(string id)
        //{
        //    var response = Request.CreateResponse(HttpStatusCode.Moved);
        //    response.Headers.Location = new Uri("http://www.yahoo.com");
        //    return response;
        //}

        // POST api/<controller>
        public void Post([FromBody]string value)
        {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}