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
using System.Web.OData;

namespace MomenticAPI.Controllers
{
    [AuthorizationKeyFilterAttribute("Token")]
    public class MomentController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/Moment
        public object GetMoment()
        {
            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.Feedback = db.Moment;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/Moment/5
        [ResponseType(typeof(Moment))]
        public async Task<object> GetMoment(int id)
        {
            Moment foundMoment = await db.Moment.FindAsync(id);

            dynamic cResponse = new ExpandoObject();
            if (foundMoment == null)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Not Found";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            cResponse.Result = "0";
            cResponse.Moment = foundMoment;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // POST: api/Moment
        [ResponseType(typeof(List<Moment>))]
        public async Task<object> PostMoment(List<Moment> momentList)
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

                foreach (Moment nextMoment in momentList)
                {
                    nextMoment.IsSafe = true;
                    nextMoment.UploadDate = DateTime.Now;
                }

                db.Moment.AddRange(momentList);
                await db.SaveChangesAsync();

                cResponse.Result = "0";
                cResponse.Description = "Moments Added";
                cResponse.Moments = momentList;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            catch (Exception ex)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Your request could not executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        [AcceptVerbs("PATCH")]
        public object PatchMoment(int id, Delta<Moment> moment)
        {
            dynamic cResponse = new ExpandoObject();

            Moment dbMoment = db.Moment.SingleOrDefault(p => p.MomentID == id);
            if (dbMoment == null)
            {
                cResponse.Result = "-1";
                cResponse.Description = "ID: " + id + ", Not Found";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            moment.Patch(dbMoment);
            db.SaveChanges();

            cResponse.Result = "0";
            cResponse.Description = "Object Updated";
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        /*
        // DELETE: api/Moment/5
        [ResponseType(typeof(Moment))]
        public async Task<IHttpActionResult> DeleteMoment(int id)
        {
            Moment moment = await db.Moment.FindAsync(id);
            if (moment == null)
            {
                return NotFound();
            }

            db.Moment.Remove(moment);
            await db.SaveChangesAsync();

            return Ok(moment);
        }
        */ 

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MomentExists(int id)
        {
            return db.Moment.Count(e => e.MomentID == id) > 0;
        }
    }
}