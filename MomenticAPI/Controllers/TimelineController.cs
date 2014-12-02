using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MomenticAPI.Models;
using System.Dynamic;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace MomenticAPI.Controllers
{
    [AuthorizationKeyFilterAttribute("Token")]
    public class TimelineController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/Timeline/5
        [ResponseType(typeof(Timeline))]
     //   [OutputCache(Duration = 3600, VaryByParam = "*")]
        public async Task<object> GetTimeline(int id)
        {
            dynamic cResponse = new ExpandoObject();

            try
            {
                // API'nin dondurecegi liste
                List<TimelineViewModel> TimelineViewModelList = new List<TimelineViewModel>();

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

                // Her Timeline icin -> TimelineViewModel Olusturur
                foreach (Timeline itemTimeline in dbTimelineList)
                {
                    // Siradaki Timeline icin Timeline'i olusturan kisiyi getirir
                    Person dbPerson = await db.Person.Where(x => x.PersonID == itemTimeline.PersonID).SingleOrDefaultAsync();

                    TimelineViewModel tModel = new TimelineViewModel();
                    tModel.TimelineID = itemTimeline.TimelineID;
                    tModel.StoryID = itemTimeline.StoryID;
                    tModel.PersonID = itemTimeline.PersonID;
                    tModel.PersonThumbnail = dbPerson.PhotoUrlThumbnail;
                    tModel.PersonUsername = dbPerson.Username;
                    tModel.DateFeed = itemTimeline.FeedDate;
                    tModel.IsReTell = itemTimeline.IsReTell;
                    tModel.CoverPhotoIndex = itemTimeline.CoverPhotoIndex;

                    // Her Timeline icin -> ilgili story getirili
                    Story dbStory = await db.Story.Where(x => x.StoryID == itemTimeline.StoryID).SingleOrDefaultAsync();

                    StoryViewModel sModel = new StoryViewModel();
                    sModel.StoryID = itemTimeline.StoryID;
                    sModel.ThemeID = dbStory.ThemeID;
                    sModel.Tag = dbStory.Tag;

                    CountStory dbCountStory = await db.CountStory.Where(x => x.StoryID == itemTimeline.StoryID).SingleOrDefaultAsync();
                    if (dbCountStory != null)
                    {
                        sModel.CountComment = dbCountStory.Comment;
                    }
                    else
                    {
                        sModel.CountComment = 0;
                    }

                    List<MomentViewModel> momentViewModelList = new List<MomentViewModel>();

                    List<int> MomentIDList = new List<int>();
                    MomentIDList.Add(dbStory.MomentID1);
                    MomentIDList.Add(dbStory.MomentID2);
                    if (dbStory.MomentID3 != null)
                    {
                        MomentIDList.Add(Convert.ToInt32(dbStory.MomentID3));
                    }
                    if (dbStory.MomentID4 != null)
                    {
                        MomentIDList.Add(Convert.ToInt32(dbStory.MomentID4));
                    }
                    if (dbStory.MomentID5 != null)
                    {
                        MomentIDList.Add(Convert.ToInt32(dbStory.MomentID5));
                    }
                    if (dbStory.MomentID6 != null)
                    {
                        MomentIDList.Add(Convert.ToInt32(dbStory.MomentID6));
                    }
                    if (dbStory.MomentID7 != null)
                    {
                        MomentIDList.Add(Convert.ToInt32(dbStory.MomentID7));
                    }

                    foreach (int item in MomentIDList)
                    {
                        Moment dbMoment = await db.Moment.Where(x => x.MomentID == item).SingleOrDefaultAsync();
                        MomentViewModel mModel = new MomentViewModel();
                        mModel.MomentID = item;
                        mModel.PhotoUrlLarge = dbMoment.PhotoUrlLarge;
                        mModel.PersonID = dbMoment.PersonID;
                        mModel.Title = dbMoment.Title;
                        mModel.IsHorizontal = dbMoment.IsHorizontal;
                        mModel.LocationString = dbMoment.LocationString;

                        CountMoment dbCountMoment = await db.CountMoment.Where(x => x.MomentID == item).SingleOrDefaultAsync();
                        if (dbCountMoment != null)
                        {
                            mModel.CountLike = dbCountMoment.LikeCount;
                        }
                        else
                        {
                            mModel.CountLike = 0;
                        }
                        momentViewModelList.Add(mModel);
                    }

                    // story'e ait moment'lar storymodel e eklenir.
                    sModel.MomentList = momentViewModelList;

                    // ekleme islemleri
                    tModel.StoryViewModel = sModel;

                    TimelineViewModelList.Add(tModel);
                }

                cResponse.Result = "0";
                cResponse.Description = "All Timeline";
                cResponse.DateNow = DateTime.Now;
                cResponse.Timeline = TimelineViewModelList;

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