using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
 using MomenticAPI.Models;

namespace MomenticAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
     //       config.SuppressDefaultHostAuthentication();
     //       config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.MapHttpAttributeRoutes();

            // Web API routes
       //     ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
       //     builder.EntitySet<Gender>("Genders");
       //     builder.EntitySet<Person>("Person");
       //     config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

        }
    }
}
