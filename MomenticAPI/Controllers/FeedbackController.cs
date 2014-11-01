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
using System.Dynamic;
using Newtonsoft.Json;
using System.Web.Http.OData;
using System.Web.Mvc;

namespace MomenticAPI.Controllers
{
    [AuthorizationKeyFilterAttribute("Token")]
    public class FeedbackController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/Feedback
        [OutputCache(Duration = 3600, VaryByParam = "*")]
        public object GetFeedback()
        {
            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.Feedback = db.Feedback;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/Feedback/5
        [ResponseType(typeof(Feedback))]
        public async Task<IHttpActionResult> GetFeedback(int id)
        {
            Feedback feedback = await db.Feedback.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

       // [AcceptVerbs("PATCH")]
        public object PatchFeedback(int id, Delta<Feedback> feedback)
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

                Feedback dbFeedback = db.Feedback.SingleOrDefault(p => p.FeedbackID == id);
                if (dbFeedback == null)
                {
                    cResponse.Result = "-1";
                    cResponse.Description = "ID: " + id + ", Not Found";
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }

                feedback.Patch(dbFeedback);
                db.SaveChanges();

                cResponse.Result = "0";
                cResponse.Description = "Object Updated";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            catch (Exception ex)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Your request could not executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        public async Task<object> PostFeedback(Feedback feedback)
        {
            dynamic cResponse = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                cResponse.Result = "-1";
                cResponse.Description = ModelState;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            feedback.IsRead = false;
            feedback.IsReplied = false;
            feedback.SentDate = DateTime.Now;

            db.Feedback.Add(feedback);
            await db.SaveChangesAsync();

            cResponse.Result = "0";
            cResponse.Description = "Object Added";
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // DELETE: api/Feedback/5
        [ResponseType(typeof(Feedback))]
        public async Task<object> DeleteFeedback(int id)
        {
            dynamic cResponse = new ExpandoObject();

            Feedback feedback = await db.Feedback.FindAsync(id);
            if (feedback == null)
            {
                cResponse.Result = "-1";
                cResponse.Description = "ID: " + id + ", Not Found";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            db.Feedback.Remove(feedback);
            await db.SaveChangesAsync();

            cResponse.Result = "0";
            cResponse.Description = "Object Deleted";
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedbackExists(int id)
        {
            return db.Feedback.Count(e => e.FeedbackID == id) > 0;
        }
    }
}