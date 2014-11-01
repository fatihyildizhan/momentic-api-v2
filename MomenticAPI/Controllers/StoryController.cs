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
    public class StoryController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/Story
        public object GetStory()
        {
            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.Story = db.Story;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/Story/5
        [ResponseType(typeof(Story))]
        public async Task<object> GetStory(int id)
        {
            Story foundStory = await db.Story.FindAsync(id);

            dynamic cResponse = new ExpandoObject();
            if (foundStory == null)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Not Found";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            cResponse.Result = "0";
            cResponse.Story = foundStory;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        [AcceptVerbs("PATCH")]
        public object PatchStory(int id, Delta<Story> story)
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

                Story dbStory = db.Story.SingleOrDefault(p => p.StoryID == id);
                if (dbStory == null)
                {
                    cResponse.Result = "-1";
                    cResponse.Description = "ID: " + id + ", Not Found";
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }

                story.Patch(dbStory);
                db.SaveChanges();

                cResponse.Result = "0";
                cResponse.Description = "Story Updated";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            catch (Exception ex)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Your request could not executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        // POST: api/Story
        [ResponseType(typeof(Story))]
        public async Task<object> PostStory(Story story)
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

                story.CreatedDate = DateTime.Now;
                story.IsReported = false;

                db.Story.Add(story);
                await db.SaveChangesAsync();

                cResponse.Result = "0";
                cResponse.Description = "Story Added";
                cResponse.Story = story;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            catch (Exception ex)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Your request could not executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        // DELETE: api/Story/5
        [ResponseType(typeof(Story))]
        public async Task<IHttpActionResult> DeleteStory(int id)
        {
            Story story = await db.Story.FindAsync(id);
            if (story == null)
            {
                return NotFound();
            }

            db.Story.Remove(story);
            await db.SaveChangesAsync();

            return Ok(story);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoryExists(int id)
        {
            return db.Story.Count(e => e.StoryID == id) > 0;
        }
    }
}