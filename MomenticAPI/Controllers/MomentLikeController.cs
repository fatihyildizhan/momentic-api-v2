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
    public class MomentLikeController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/MomentLike
        public object GetMomentLike()
        {
            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.Data = db.MomentLike;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // POST: api/MomentLike
        [ResponseType(typeof(MomentLike))]
        public async Task<object> PostMomentLike(MomentLike momentLike)
        {
            dynamic cResponse = new ExpandoObject();

            if (!ModelState.IsValid)
            {
                cResponse.Result = "-1";
                cResponse.Description = ModelState;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            int resultLike = await db.MomentLike.CountAsync(x => x.PersonID == momentLike.PersonID && x.MomentID == momentLike.MomentID);
            if (resultLike > 0)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Already Liked";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            momentLike.LikeDate = DateTime.Now;

            db.MomentLike.Add(momentLike);
            await db.SaveChangesAsync();

            cResponse.Result = "0";
            cResponse.Description = "Like added to database";
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // DELETE: api/MomentLike/5
        [ResponseType(typeof(MomentLike))]
        public async Task<IHttpActionResult> DeleteMomentLike(int id)
        {
            MomentLike momentLike = await db.MomentLike.FindAsync(id);
            if (momentLike == null)
            {
                return NotFound();
            }

            db.MomentLike.Remove(momentLike);
            await db.SaveChangesAsync();

            return Ok(momentLike);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MomentLikeExists(int id)
        {
            return db.MomentLike.Count(e => e.MomentID == id) > 0;
        }
    }
}