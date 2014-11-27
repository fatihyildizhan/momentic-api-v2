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
using Newtonsoft.Json;
using System.Dynamic;

namespace MomenticAPI.Controllers
{
    [AuthorizationKeyFilterAttribute("Token")]
    public class CommentController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/Comment
        public IQueryable<Comment> GetComment()
        {
            return db.Comment;
        }

        [OutputCache(Duration = 3600, VaryByParam = "*")]
        // GET: api/Comment/5
        [ResponseType(typeof(Comment))]
        public async Task<object> GetComment(int id)
        {
            dynamic cResponse = new ExpandoObject();

            List<CommentViewModel> commentViewModels = new List<CommentViewModel>();
            //.OrderByDescending(x => x.CommentDate) ters siralamak gerekli mi
            List<Comment> dbComments = await db.Comment.Where(x => x.StoryID == id).ToListAsync();

            foreach (Comment item in dbComments)
            {
                Person dbPerson = await db.Person.Where(x => x.PersonID == item.PersonID).SingleOrDefaultAsync();

                CommentViewModel cModel = new CommentViewModel();
                cModel.CommentDate = item.CommentDate;
                cModel.CommentID = item.CommentID;
                cModel.PersonID = item.PersonID;
                cModel.PersonThumbnail = dbPerson.PhotoUrlThumbnail;
                cModel.PersonUsername = dbPerson.Username;
                cModel.StoryID = item.StoryID;
                cModel.Text = item.Text;

                commentViewModels.Add(cModel);
            }

            cResponse.Result = "0";
            cResponse.DateNow = DateTime.Now;
            cResponse.Data = commentViewModels;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // PUT: api/Comment/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.CommentID)
            {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comment
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> PostComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comment.Add(comment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = comment.CommentID }, comment);
        }

        // DELETE: api/Comment/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            Comment comment = await db.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comment.Remove(comment);
            await db.SaveChangesAsync();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comment.Count(e => e.CommentID == id) > 0;
        }
    }
}