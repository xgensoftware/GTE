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
    public class VelocifyAPIController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string companyName, string phoneNum)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.SeeOther);

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
                r.GetBy(companyName,APIProvider.VELOCIFY.ToString());
            }
            catch
            {
                HttpError e = new HttpError(string.Format("Company {0} not found", companyName));
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e);
            }
            
            //VelocifyService s = new VelocifyService("brian@bestrate-insurance.com", "7164789226");
            VelocifyService s = new VelocifyService(r.APIUserName, r.APIPassword);
            int leadId = s.GetContactIdByPhone(phoneNum);

            if(leadId == -1)
            {
                response.Headers.Location = new Uri("https://lm.velocify.com/Web/LeadAddEdit.aspx");
            }
            else
                response.Headers.Location = new Uri(string.Format("https://lm.velocify.com/Web/LeadAddEdit.aspx?LeadId={0}", leadId));

            return response;
        }

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
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