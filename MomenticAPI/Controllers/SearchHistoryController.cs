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
    public class SearchHistoryController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/SearchHistory
        [OutputCache(Duration = 3600, VaryByParam = "*")]
        public object GetSearchHistory()
        {
            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.SearchHistory = db.SearchHistory;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/SearchHistory/5
        [ResponseType(typeof(SearchHistory))]
        public async Task<IHttpActionResult> GetSearchHistory(int id)
        {
            SearchHistory searchHistory = await db.SearchHistory.FindAsync(id);
            if (searchHistory == null)
            {
                return NotFound();
            }

            return Ok(searchHistory);
        }

        // PUT: api/SearchHistory/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSearchHistory(int id, SearchHistory searchHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != searchHistory.SearchID)
            {
                return BadRequest();
            }

            db.Entry(searchHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchHistoryExists(id))
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

        // POST: api/SearchHistory
        [ResponseType(typeof(SearchHistory))]
        public async Task<object> PostSearchHistory(SearchHistory searchHistory)
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

                searchHistory.SearchDate = DateTime.Now;

                // will count 
                searchHistory.CountResult = 0;

                db.SearchHistory.Add(searchHistory);
                await db.SaveChangesAsync();

                cResponse.Result = "0";
                cResponse.Description = "Search History added to database";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            catch
            {
                cResponse.Result = "-1";
                cResponse.Description = "Exception, your request could not be executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        // DELETE: api/SearchHistory/5
        [ResponseType(typeof(SearchHistory))]
        public async Task<IHttpActionResult> DeleteSearchHistory(int id)
        {
            SearchHistory searchHistory = await db.SearchHistory.FindAsync(id);
            if (searchHistory == null)
            {
                return NotFound();
            }

            db.SearchHistory.Remove(searchHistory);
            await db.SaveChangesAsync();

            return Ok(searchHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SearchHistoryExists(int id)
        {
            return db.SearchHistory.Count(e => e.SearchID == id) > 0;
        }
    }
}