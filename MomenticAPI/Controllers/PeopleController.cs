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
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using MomenticAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Person>("People");
    builder.EntitySet<Activity>("Activity"); 
    builder.EntitySet<Comment>("Comment"); 
    builder.EntitySet<Device>("Device"); 
    builder.EntitySet<Feedback>("Feedback"); 
    builder.EntitySet<Gender>("Gender"); 
    builder.EntitySet<Moment>("Moment"); 
    builder.EntitySet<MomentLike>("MomentLike"); 
    builder.EntitySet<Notification>("Notification"); 
    builder.EntitySet<PersonRole>("PersonRole"); 
    builder.EntitySet<PersonFollowing>("PersonFollowing"); 
    builder.EntitySet<Report>("Report"); 
    builder.EntitySet<SearchHistory>("SearchHistory"); 
    builder.EntitySet<Story>("Story"); 
    builder.EntitySet<Timeline>("Timeline"); 
    builder.EntitySet<Theme>("Theme"); 
    builder.EntitySet<PersonToken>("PersonToken"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PeopleController : ODataController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: odata/People
        [EnableQuery]
        public IQueryable<Person> GetPeople()
        {
            return db.Person;
        }

        // GET: odata/People(5)
        [EnableQuery]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(db.Person.Where(person => person.PersonID == key));
        }

        // PUT: odata/People(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Person> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = await db.Person.FindAsync(key);
            if (person == null)
            {
                return NotFound();
            }

            patch.Put(person);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        // POST: odata/People
        public async Task<IHttpActionResult> Post(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Person.Add(person);
            await db.SaveChangesAsync();

            return Created(person);
        }

        // PATCH: odata/People(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Person> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = await db.Person.FindAsync(key);
            if (person == null)
            {
                return NotFound();
            }

            patch.Patch(person);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        // DELETE: odata/People(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Person person = await db.Person.FindAsync(key);
            if (person == null)
            {
                return NotFound();
            }

            db.Person.Remove(person);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/People(5)/Activity
        [EnableQuery]
        public IQueryable<Activity> GetActivity([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Activity);
        }

        // GET: odata/People(5)/Activity1
        [EnableQuery]
        public IQueryable<Activity> GetActivity1([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Activity1);
        }

        // GET: odata/People(5)/Comment
        [EnableQuery]
        public IQueryable<Comment> GetComment([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Comment);
        }

        // GET: odata/People(5)/Device
        [EnableQuery]
        public IQueryable<Device> GetDevice([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Device);
        }

        // GET: odata/People(5)/Feedback
        [EnableQuery]
        public IQueryable<Feedback> GetFeedback([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Feedback);
        }

        // GET: odata/People(5)/Gender
        [EnableQuery]
        public SingleResult<Gender> GetGender([FromODataUri] int key)
        {
            return SingleResult.Create(db.Person.Where(m => m.PersonID == key).Select(m => m.Gender));
        }

        // GET: odata/People(5)/Moment
        [EnableQuery]
        public IQueryable<Moment> GetMoment([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Moment);
        }

        // GET: odata/People(5)/MomentLike
        [EnableQuery]
        public IQueryable<MomentLike> GetMomentLike([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.MomentLike);
        }

        // GET: odata/People(5)/Notification
        [EnableQuery]
        public IQueryable<Notification> GetNotification([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Notification);
        }

        // GET: odata/People(5)/PersonRole
        [EnableQuery]
        public SingleResult<PersonRole> GetPersonRole([FromODataUri] int key)
        {
            return SingleResult.Create(db.Person.Where(m => m.PersonID == key).Select(m => m.PersonRole));
        }

        // GET: odata/People(5)/PersonFollowing
        [EnableQuery]
        public IQueryable<PersonFollowing> GetPersonFollowing([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.PersonFollowing);
        }

        // GET: odata/People(5)/PersonFollowing1
        [EnableQuery]
        public IQueryable<PersonFollowing> GetPersonFollowing1([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.PersonFollowing1);
        }

        // GET: odata/People(5)/Report
        [EnableQuery]
        public IQueryable<Report> GetReport([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Report);
        }

        // GET: odata/People(5)/SearchHistory
        [EnableQuery]
        public IQueryable<SearchHistory> GetSearchHistory([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.SearchHistory);
        }

        // GET: odata/People(5)/Story
        [EnableQuery]
        public IQueryable<Story> GetStory([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Story);
        }

        // GET: odata/People(5)/Timeline
        [EnableQuery]
        public IQueryable<Timeline> GetTimeline([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Timeline);
        }

        // GET: odata/People(5)/Theme
        [EnableQuery]
        public IQueryable<Theme> GetTheme([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.Theme);
        }

        // GET: odata/People(5)/PersonToken
        [EnableQuery]
        public IQueryable<PersonToken> GetPersonToken([FromODataUri] int key)
        {
            return db.Person.Where(m => m.PersonID == key).SelectMany(m => m.PersonToken);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int key)
        {
            return db.Person.Count(e => e.PersonID == key) > 0;
        }
    }
}
