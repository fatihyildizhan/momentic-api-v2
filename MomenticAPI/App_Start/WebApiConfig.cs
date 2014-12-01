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
                routeTemplate: "api/{controller}/{id}/{secondary}",
                defaults: new { id = RouteParameter.Optional, secondary = RouteParameter.Optional }
            );

          
            //config.MapHttpAttributeRoutes();

            // Web API routes
            //     ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //     builder.EntitySet<Gender>("Genders");
            //     builder.EntitySet<Person>("Person");
            //     config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());


            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Activity>("Activity");
            builder.EntitySet<ActivityCategory>("ActivityCategory");
            builder.EntitySet<AppVersion>("AppVersion");
            builder.EntitySet<Comment>("Comment");
            builder.EntitySet<Moment>("Moment");
            builder.EntitySet<Person>("Person");
            builder.EntitySet<Report>("Report");
            builder.EntitySet<StoryCategory>("StoryCategory");
            builder.EntitySet<Theme>("Theme");
            builder.EntitySet<Timeline>("Timeline");
            builder.EntitySet<Comment>("Comment");
            builder.EntitySet<Device>("Device");
            builder.EntitySet<DeviceType>("DeviceType");
            builder.EntitySet<DeviceOs>("DeviceOs");
            builder.EntitySet<OsVersion>("OsVersion");
            builder.EntitySet<Feedback>("Feedback");
            builder.EntitySet<FeedbackCategory>("FeedbackCategory");
            builder.EntitySet<Gender>("Gender");
            builder.EntitySet<Language>("Language");
            builder.EntitySet<MomentLike>("MomentLike");
            builder.EntitySet<Notification>("Notification");
            builder.EntitySet<PersonRole>("PersonRole");
            builder.EntitySet<PersonFollowing>("PersonFollowing");
            builder.EntitySet<SearchHistory>("SearchHistory");
            builder.EntitySet<Story>("Stories");
            builder.EntitySet<Timeline>("Timeline");
            builder.EntitySet<PersonToken>("PersonToken");
            builder.EntitySet<NotificationType>("NotificationType");
            builder.EntitySet<NotificationCase>("NotificationCase");

            builder.EntitySet<CountMoment>("CountMoment");
            builder.EntitySet<CountPerson>("CountPerson");
            builder.EntitySet<CountStory>("CountStory");
            builder.EntitySet<ReTell>("ReTell");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
