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
    public class TimelineController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/Timeline/5
        [ResponseType(typeof(Timeline))]
        public async Task<object> GetTimeline(int id)
        {
            dynamic cResponse = new ExpandoObject();

            try
            {
                // takip listesi icin gerekli
                List<PersonFollowing> dbPFollowingList = await db.PersonFollowing.Where(x => x.PersonID == id && x.IsAccepted == true).ToListAsync();

                // takip ettigi kisilerin ve kendi id'sinin bulundugu ID listesi
                List<int> SPersonIDList = new List<int>();

                // kendi olusturdugu timeline'in gelmesi icin kisinin id si eklenir.
                SPersonIDList.Add(id);

                // takip ettigi arkadaslarinin da aktivitelerini getirmek icin ID listesi hazirlanir
                foreach (PersonFollowing item in dbPFollowingList)
                {
                    SPersonIDList.Add(item.SecondaryPersonID);
                }

                // Tum ID filtreli ve eklenme zamanina gore ters siralandirilmis Timeline listesi
                List<Timeline> dbTimelineList = await db.Timeline.Where(x => SPersonIDList.Contains(x.PersonID)).OrderByDescending(x => x.FeedDate).ToListAsync();

                cResponse.Result = "0";
                cResponse.Description = "All Timeline";
                cResponse.Timeline = dbTimelineList;

                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            catch (Exception ex)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Your request could not executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        // POST: api/Timeline
        [ResponseType(typeof(Timeline))]
        public async Task<IHttpActionResult> PostTimeline(Timeline timeline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Timeline.Add(timeline);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = timeline.TimelineID }, timeline);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimelineExists(int id)
        {
            return db.Timeline.Count(e => e.TimelineID == id) > 0;
        }
    }
}