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
    builder.EntitySet<Moment>("Moments");
    builder.EntitySet<Person>("Person"); 
    builder.EntitySet<MomentLike>("MomentLike"); 
    builder.EntitySet<Story>("Story"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MomentsController : ODataController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: odata/Moments
        [EnableQuery]
        public IQueryable<Moment> GetMoments()
        {
            return db.Moment;
        }

        // GET: odata/Moments(5)
        [EnableQuery]
        public SingleResult<Moment> GetMoment([FromODataUri] int key)
        {
            return SingleResult.Create(db.Moment.Where(moment => moment.MomentID == key));
        }

        // PUT: odata/Moments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Moment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Moment moment = await db.Moment.FindAsync(key);
            if (moment == null)
            {
                return NotFound();
            }

            patch.Put(moment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MomentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(moment);
        }

        // POST: odata/Moments
        public async Task<IHttpActionResult> Post(Moment moment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Moment.Add(moment);
            await db.SaveChangesAsync();

            return Created(moment);
        }

        // PATCH: odata/Moments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Moment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Moment moment = await db.Moment.FindAsync(key);
            if (moment == null)
            {
                return NotFound();
            }

            patch.Patch(moment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MomentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(moment);
        }

        // DELETE: odata/Moments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Moment moment = await db.Moment.FindAsync(key);
            if (moment == null)
            {
                return NotFound();
            }

            db.Moment.Remove(moment);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Moments(5)/Person
        [EnableQuery]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(db.Moment.Where(m => m.MomentID == key).Select(m => m.Person));
        }

        // GET: odata/Moments(5)/MomentLike
        [EnableQuery]
        public IQueryable<MomentLike> GetMomentLike([FromODataUri] int key)
        {
            return db.Moment.Where(m => m.MomentID == key).SelectMany(m => m.MomentLike);
        }

        // GET: odata/Moments(5)/Story
        [EnableQuery]
        public IQueryable<Story> GetStory([FromODataUri] int key)
        {
            return db.Moment.Where(m => m.MomentID == key).SelectMany(m => m.Story);
        }

        // GET: odata/Moments(5)/Story1
        [EnableQuery]
        public IQueryable<Story> GetStory1([FromODataUri] int key)
        {
            return db.Moment.Where(m => m.MomentID == key).SelectMany(m => m.Story1);
        }

        // GET: odata/Moments(5)/Story2
        [EnableQuery]
        public IQueryable<Story> GetStory2([FromODataUri] int key)
        {
            return db.Moment.Where(m => m.MomentID == key).SelectMany(m => m.Story2);
        }

        // GET: odata/Moments(5)/Story3
        [EnableQuery]
        public IQueryable<Story> GetStory3([FromODataUri] int key)
        {
            return db.Moment.Where(m => m.MomentID == key).SelectMany(m => m.Story3);
        }

        // GET: odata/Moments(5)/Story4
        [EnableQuery]
        public IQueryable<Story> GetStory4([FromODataUri] int key)
        {
            return db.Moment.Where(m => m.MomentID == key).SelectMany(m => m.Story4);
        }

        // GET: odata/Moments(5)/Story5
        [EnableQuery]
        public IQueryable<Story> GetStory5([FromODataUri] int key)
        {
            return db.Moment.Where(m => m.MomentID == key).SelectMany(m => m.Story5);
        }

        // GET: odata/Moments(5)/Story6
        [EnableQuery]
        public IQueryable<Story> GetStory6([FromODataUri] int key)
        {
            return db.Moment.Where(m => m.MomentID == key).SelectMany(m => m.Story6);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MomentExists(int key)
        {
            return db.Moment.Count(e => e.MomentID == key) > 0;
        }
    }
}
