﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GTE_API.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Action",
                routeTemplate: "api/{controller}/{GetContactByPhone}/{phoneNum}",
                defaults: new { phoneNum = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "GetContactByPhone",
               routeTemplate: "api/{controller}/{GetContactByPhone}/{companyName}/{phoneNum}"
           );

        }
    }
}