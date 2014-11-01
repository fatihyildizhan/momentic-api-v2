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

namespace MomenticAPI.Controllers
{
    [AuthorizationKeyFilterAttribute("Token")]
    public class ActivityCategoryController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/ActivityCategory
        [OutputCache(Duration = 3600, VaryByParam = "*")]
        public object GetActivityCategory()
        {
            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.ActivityCategory = db.ActivityCategory;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/ActivityCategory/5
        [ResponseType(typeof(ActivityCategory))]
        public async Task<IHttpActionResult> GetActivityCategory(int id)
        {
            ActivityCategory activityCategory = await db.ActivityCategory.FindAsync(id);
            if (activityCategory == null)
            {
                return NotFound();
            }

            return Ok(activityCategory);
        }

        // PUT: api/ActivityCategory/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivityCategory(int id, ActivityCategory activityCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityCategory.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(activityCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ActivityCategory
        [ResponseType(typeof(ActivityCategory))]
        public async Task<object> PostActivityCategory(ActivityCategory activityCategory)
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

                db.ActivityCategory.Add(activityCategory);
                await db.SaveChangesAsync();

                cResponse.Result = "0";
                cResponse.Description = "Activity Category added to database";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            catch
            {
                cResponse.Result = "-1";
                cResponse.Description = "Exception, your request could not be executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        // DELETE: api/ActivityCategory/5
        [ResponseType(typeof(ActivityCategory))]
        public async Task<IHttpActionResult> DeleteActivityCategory(int id)
        {
            ActivityCategory activityCategory = await db.ActivityCategory.FindAsync(id);
            if (activityCategory == null)
            {
                return NotFound();
            }

            db.ActivityCategory.Remove(activityCategory);
            await db.SaveChangesAsync();

            return Ok(activityCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityCategoryExists(int id)
        {
            return db.ActivityCategory.Count(e => e.CategoryID == id) > 0;
        }
    }
}