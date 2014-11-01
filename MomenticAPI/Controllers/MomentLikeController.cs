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

namespace MomenticAPI.Controllers
{
    [AuthorizationKeyFilterAttribute("Token")]
    public class MomentLikeController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/MomentLike
        public IQueryable<MomentLike> GetMomentLike()
        {
            return db.MomentLike;
        }

        // GET: api/MomentLike/5
        [ResponseType(typeof(MomentLike))]
        public async Task<IHttpActionResult> GetMomentLike(int id)
        {
            MomentLike momentLike = await db.MomentLike.FindAsync(id);
            if (momentLike == null)
            {
                return NotFound();
            }

            return Ok(momentLike);
        }

        // POST: api/MomentLike
        [ResponseType(typeof(MomentLike))]
        public async Task<IHttpActionResult> PostMomentLike(MomentLike momentLike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MomentLike.Add(momentLike);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MomentLikeExists(momentLike.MomentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = momentLike.MomentID }, momentLike);
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