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
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using MomenticAPI.Models;
namespace MomenticAPI.Controllers
{

    [AuthorizationKeyFilterAttribute("Token")]
    public class StoriesController : ODataController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: odata/Stories
        [EnableQuery]
        public IQueryable<Story> GetStories()
        {
            return db.Story;
        }

        // GET: odata/Stories(5)
        [EnableQuery]
        public SingleResult<Story> GetStory([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(story => story.StoryID == key));
        }

        // PUT: odata/Stories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Story> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Story story = await db.Story.FindAsync(key);
            if (story == null)
            {
                return NotFound();
            }

            patch.Put(story);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(story);
        }

        // POST: odata/Stories
        public async Task<IHttpActionResult> Post(Story story)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Story.Add(story);
            await db.SaveChangesAsync();

            return Created(story);
        }

        // PATCH: odata/Stories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Story> patch)
        {
            Story dbStory = db.Story.SingleOrDefault(p => p.StoryID == key);
            if (dbStory == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            patch.Patch(dbStory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: odata/Stories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Story story = await db.Story.FindAsync(key);
            if (story == null)
            {
                return NotFound();
            }

            db.Story.Remove(story);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Stories(5)/Comment
        [EnableQuery]
        public IQueryable<Comment> GetComment([FromODataUri] int key)
        {
            return db.Story.Where(m => m.StoryID == key).SelectMany(m => m.Comment);
        }

        // GET: odata/Stories(5)/Moment
        [EnableQuery]
        public SingleResult<Moment> GetMoment([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(m => m.StoryID == key).Select(m => m.Moment));
        }

        // GET: odata/Stories(5)/Moment1
        [EnableQuery]
        public SingleResult<Moment> GetMoment1([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(m => m.StoryID == key).Select(m => m.Moment1));
        }

        // GET: odata/Stories(5)/Moment2
        [EnableQuery]
        public SingleResult<Moment> GetMoment2([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(m => m.StoryID == key).Select(m => m.Moment2));
        }

        // GET: odata/Stories(5)/Moment3
        [EnableQuery]
        public SingleResult<Moment> GetMoment3([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(m => m.StoryID == key).Select(m => m.Moment3));
        }

        // GET: odata/Stories(5)/Moment4
        [EnableQuery]
        public SingleResult<Moment> GetMoment4([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(m => m.StoryID == key).Select(m => m.Moment4));
        }

        // GET: odata/Stories(5)/Moment5
        [EnableQuery]
        public SingleResult<Moment> GetMoment5([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(m => m.StoryID == key).Select(m => m.Moment5));
        }

        // GET: odata/Stories(5)/Moment6
        [EnableQuery]
        public SingleResult<Moment> GetMoment6([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(m => m.StoryID == key).Select(m => m.Moment6));
        }

        // GET: odata/Stories(5)/Person
        [EnableQuery]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(m => m.StoryID == key).Select(m => m.Person));
        }

        // GET: odata/Stories(5)/Report
        [EnableQuery]
        public IQueryable<Report> GetReport([FromODataUri] int key)
        {
            return db.Story.Where(m => m.StoryID == key).SelectMany(m => m.Report);
        }

        // GET: odata/Stories(5)/StoryCategory
        [EnableQuery]
        public SingleResult<StoryCategory> GetStoryCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(m => m.StoryID == key).Select(m => m.StoryCategory));
        }

        // GET: odata/Stories(5)/Theme
        [EnableQuery]
        public SingleResult<Theme> GetTheme([FromODataUri] int key)
        {
            return SingleResult.Create(db.Story.Where(m => m.StoryID == key).Select(m => m.Theme));
        }

        // GET: odata/Stories(5)/Timeline
        [EnableQuery]
        public IQueryable<Timeline> GetTimeline([FromODataUri] int key)
        {
            return db.Story.Where(m => m.StoryID == key).SelectMany(m => m.Timeline);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoryExists(int key)
        {
            return db.Story.Count(e => e.StoryID == key) > 0;
        }
    }
}
