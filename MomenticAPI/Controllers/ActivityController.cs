using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MomenticAPI.Models;
using System.Web.Mvc;
using System.Dynamic;
using Newtonsoft.Json;
using System.Web.OData;

namespace MomenticAPI.Controllers
{
    [AuthorizationKeyFilterAttribute("Token")]
    public class ActivityController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/Activitiy
        [OutputCache(Duration = 3600, VaryByParam = "*")]
        public object GetActivity()
        {
            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.DateNow = DateTime.Now;
            cResponse.Device = db.Activity;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/Activitiy/5
        [ResponseType(typeof(Activity))]
        public async Task<object> GetActivity(int id)
        {
            dynamic cResponse = new ExpandoObject();

            List<Activity> FoundActiviy = await db.Activity.Where(x => x.PersonID == id && x.IsHidden == false).ToListAsync();
            if (FoundActiviy.Count > 0)
            {
                cResponse.Result = "0";
                cResponse.Activity = FoundActiviy;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            else
            {
                cResponse.Result = "-1";
                cResponse.Description = "There is no activity";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        // PUT: api/Activitiy/5
        //[ResponseType(typeof(void))]
        public object PatchActivity(int id, Delta<Activity> activity)
        {
            dynamic cResponse = new ExpandoObject();

            Activity dbActivity = db.Activity.SingleOrDefault(p => p.ActivityID == id);
            if (dbActivity == null)
            {
                cResponse.Result = "-1";
                cResponse.Description = "ID: " + id + ", Not Found";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            activity.Patch(dbActivity);
            db.SaveChanges();

            cResponse.Result = "0";
            cResponse.Description = "Activity Updated";
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // POST: api/Activitiy
        [ResponseType(typeof(Activity))]
        public async Task<object> PostActivity(Activity activity)
        {
            dynamic cResponse = new ExpandoObject();
            try
            {
                if (!ModelState.IsValid)
                {
                    cResponse.Result = "-1";
                    cResponse.Description = ModelState;
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }

                activity.IsHidden = false;
                activity.ActivityDate = DateTime.Now;
                db.Activity.Add(activity);
                await db.SaveChangesAsync();

                cResponse.Result = "0";
                cResponse.Description = "Activity added to database";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            catch
            {
                cResponse.Result = "-1";
                cResponse.Description = "Exception, your request could not be executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        // DELETE: api/Activitiy/5
        [ResponseType(typeof(Activity))]
        public async Task<IHttpActionResult> DeleteActivity(int id)
        {
            Activity activity = await db.Activity.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            db.Activity.Remove(activity);
            await db.SaveChangesAsync();

            return Ok(activity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityExists(int id)
        {
            return db.Activity.Count(e => e.ActivityID == id) > 0;
        }
    }
}