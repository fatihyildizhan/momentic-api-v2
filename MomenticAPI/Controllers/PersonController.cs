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
using System.Web.Mvc;
using System.Web.Http.OData;

namespace MomenticAPI.Controllers
{
    public class PersonController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/Person
        [OutputCache(Duration = 3600, VaryByParam = "*")]
        [AuthorizationKeyFilterAttribute("Token")]
        public object GetPerson()
        {
            dynamic cResponse = new ExpandoObject();
            cResponse.Result = "0";
            cResponse.Person = db.Person;

            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/Person/5
        [ResponseType(typeof(Person))]
        [AuthorizationKeyFilterAttribute("Token")]
        public async Task<object> GetPerson(int id)
        {
            Person person = await db.Person.FindAsync(id);

            dynamic cResponse = new ExpandoObject();
            if (person == null)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Not Found";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            cResponse.Result = "0";
            cResponse.Person = person;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // POST: api/Person
        [ResponseType(typeof(Person))]
        public async Task<object> PostPerson(Person person)
        {
            dynamic cResponse = new ExpandoObject();

            if (!ModelState.IsValid)
            {
                cResponse.Result = "-1";
                cResponse.Description = ModelState;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            int resultEmail = await db.Person.CountAsync(x => x.Email == person.Email);
            if (resultEmail > 0)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Email is not available";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            int resultUsername = await db.Person.CountAsync(x => x.Username == person.Username);
            if (resultUsername > 0)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Username is not available";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            person.CountDevice = 0;
            person.CountFeedback = 0;
            person.CountFollower = 0;
            person.CountFollowing = 0;
            person.CountLike = 0;
            person.CountMoment = 0;
            person.CountReport = 0;
            person.CountReportedStory = 0;
            person.CountStory = 0;
            person.IsEmailAllowed = true;
            person.IsPrivate = false;
            person.IsPushAllowed = true;
            person.IsSuspended = false;
            person.LastLoginDate = DateTime.Now;
            person.PersonRoleID = 1;
            person.RegisterDate = DateTime.Now;
            person.GenderID = 3;

            db.Person.Add(person);
            await db.SaveChangesAsync();

            string tokenResult = BasicHelper.TokenCreate(person.PersonID);

            if (tokenResult == "-1")
            {
                cResponse.Result = "-1";
                cResponse.Message = "Token could not created";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            cResponse.Result = "0";
            cResponse.Token = tokenResult;
            cResponse.Description = "Person added to database";
            cResponse.Person = person;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // [AcceptVerbs("PATCH")]
        [AuthorizationKeyFilterAttribute("Token")]
        public object PatchPerson(int id, Delta<Person> person)
        {
            dynamic cResponse = new ExpandoObject();

            Person dbPerson = db.Person.SingleOrDefault(p => p.PersonID == id);
            if (dbPerson == null)
            {
                cResponse.Result = "-1";
                cResponse.Description = "ID: " + id + ", Not Found";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            person.Patch(dbPerson);
            db.SaveChanges();

            cResponse.Result = "0";
            cResponse.Description = "Person Updated";
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // DELETE: api/Person/5
        [ResponseType(typeof(Person))]
        [AuthorizationKeyFilterAttribute("Token")]
        public async Task<IHttpActionResult> DeletePerson(int id)
        {
            Person person = await db.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            db.Person.Remove(person);
            await db.SaveChangesAsync();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.Person.Count(e => e.PersonID == id) > 0;
        }
    }
}