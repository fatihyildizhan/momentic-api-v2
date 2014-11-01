using MomenticAPI.Models;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace MomenticAPI
{
   // [AttributeUsage(AttributeTargets.Method)]
    public sealed class AuthorizationKeyFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private MomenticEntities db = new MomenticEntities();
        public AuthorizationKeyFilterAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; private set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var value = actionContext.Request.Headers.Authorization;

            try
            {
                PersonToken pt = db.PersonToken.Where(x => x.Token == value.Scheme).SingleOrDefault();
                if (pt != null)
                {
                    pt.DateLastLogin = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    //dynamic cResponse = new ExpandoObject();
                    //cResponse.Result = "-1";
                    //cResponse.Feedback = "Valid token could not found";
                    //JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));

                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            actionContext.ActionArguments[ParameterName] = value;
        }
    }
}