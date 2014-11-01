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

namespace MomenticAPI.Controllers
{
    [AuthorizationKeyFilterAttribute("Token")]
    public class FeedbackCategoryController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/FeedbackCategory
        public object GetFeedbackCategory()
        {
            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.FeedbackCategory = db.FeedbackCategory;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/FeedbackCategory/5
        [ResponseType(typeof(FeedbackCategory))]
        public async Task<IHttpActionResult> GetFeedbackCategory(int id)
        {
            FeedbackCategory feedbackCategory = await db.FeedbackCategory.FindAsync(id);
            if (feedbackCategory == null)
            {
                return NotFound();
            }

            return Ok(feedbackCategory);
        }

        // PUT: api/FeedbackCategory/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFeedbackCategory(int id, FeedbackCategory feedbackCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedbackCategory.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(feedbackCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackCategoryExists(id))
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

        // POST: api/FeedbackCategory
        [ResponseType(typeof(FeedbackCategory))]
        public async Task<object> PostFeedbackCategory(FeedbackCategory feedbackCategory)
        {
            dynamic cResponse = new ExpandoObject();
            if (!ModelState.IsValid)
            {
                cResponse.Result = "-1";
                cResponse.Description = ModelState;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            db.FeedbackCategory.Add(feedbackCategory);
            await db.SaveChangesAsync();

            cResponse.Result = "0";
            cResponse.Description = "Object Added";
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // DELETE: api/FeedbackCategory/5
        [ResponseType(typeof(FeedbackCategory))]
        public async Task<object> DeleteFeedbackCategory(int id)
        {
            dynamic cResponse = new ExpandoObject();
            FeedbackCategory feedbackCategory = await db.FeedbackCategory.FindAsync(id);
            if (feedbackCategory == null)
            {
                cResponse.Result = "-1";
                cResponse.Description = "ID: " + id + ", Not Found";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            db.FeedbackCategory.Remove(feedbackCategory);
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

        private bool FeedbackCategoryExists(int id)
        {
            return db.FeedbackCategory.Count(e => e.CategoryID == id) > 0;
        }
    }
}