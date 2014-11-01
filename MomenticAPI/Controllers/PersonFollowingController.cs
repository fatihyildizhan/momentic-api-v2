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
    public class PersonFollowingController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/PersonFollowing
        public object GetPersonFollowing()
        {
            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.Feedback = db.PersonFollowing;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/PersonFollowing/5
        [ResponseType(typeof(PersonFollowing))]
        public async Task<object> GetPersonFollowing(int id)
        {
            dynamic cResponse = new ExpandoObject();

            List<PersonFollowing> FoundPersonFollowing = await db.PersonFollowing.Where(x => x.PersonID == id && x.IsAccepted == true).ToListAsync();
            if (FoundPersonFollowing.Count > 0)
            {
                cResponse.Result = "0";
                cResponse.Device = FoundPersonFollowing;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            else
            {
                cResponse.Result = "-1";
                cResponse.Device = "There is no any friend";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        // PUT: api/PersonFollowing/5
        [AcceptVerbs("PATCH")]
        public object PatchPersonFollowing(int id, int secondary, Delta<PersonFollowing> personFollowing)
        {
            dynamic cResponse = new ExpandoObject();

            PersonFollowing dbFollowing = db.PersonFollowing.SingleOrDefault(p => p.PersonID == id && p.SecondaryPersonID == secondary);
            if (dbFollowing == null)
            {
                cResponse.Result = "-1";
                cResponse.Description = "ID: " + id + ", Not Found";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            dbFollowing.DateAccepted = DateTime.Now;

            personFollowing.Patch(dbFollowing);
            db.SaveChanges();

            cResponse.Result = "0";
            cResponse.Description = "Object Updated";
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // POST: api/PersonFollowing
        [ResponseType(typeof(PersonFollowing))]
        public async Task<object> PostPersonFollowing(PersonFollowing personFollowing)
        {
            dynamic cResponse = new ExpandoObject();

            if (personFollowing.PersonID == personFollowing.SecondaryPersonID)
            {
                 cResponse.Result = "-1";
                cResponse.Description = "You cannot follow yourself";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    cResponse.Result = "-1";
                    cResponse.Description = ModelState;
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }

                personFollowing.DateRequest = DateTime.Now;
                personFollowing.IsAccepted = false;

                db.PersonFollowing.Add(personFollowing);
                await db.SaveChangesAsync();

                cResponse.Result = "0";
                cResponse.Description = "Person Following Added";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            catch
            {
                cResponse.Result = "-1";
                cResponse.Description = "Exception, your request could not be executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        // DELETE: api/PersonFollowing/5
        [ResponseType(typeof(PersonFollowing))]
        public async Task<IHttpActionResult> DeletePersonFollowing(int id)
        {
            PersonFollowing personFollowing = await db.PersonFollowing.FindAsync(id);
            if (personFollowing == null)
            {
                return NotFound();
            }

            db.PersonFollowing.Remove(personFollowing);
            await db.SaveChangesAsync();

            return Ok(personFollowing);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonFollowingExists(int id)
        {
            return db.PersonFollowing.Count(e => e.PersonID == id) > 0;
        }
    }
}