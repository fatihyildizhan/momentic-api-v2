using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MomenticAPI.Models;
using System.Dynamic;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace MomenticAPI.Controllers
{
    [AuthorizationKeyFilterAttribute("Token")]

    public class MomentLikeGETController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        [OutputCache(Duration = 3600, VaryByParam = "*")]
        public async Task<object> PostMomentLike(MomentLike like)
        {
            List<MomentLike> likes = await db.MomentLike.Where(p => p.MomentID == like.MomentID).ToListAsync();
            List<int> IDList = new List<int>();

            foreach (MomentLike item in likes)
            {
                IDList.Add(item.PersonID);
            }

            List<Person> people = await db.Person.Where(t => IDList.Contains(t.PersonID)).ToListAsync();
            List<MomentLikeViewModel> likeModels = new List<MomentLikeViewModel>();
            foreach (Person item in people)
            {
                MomentLikeViewModel model = new MomentLikeViewModel();
                model.MomentID = like.MomentID;
                model.PersonID = item.PersonID;
                model.PersonFirstName = item.FirstName;
                model.PersonLastName = item.LastName;
                model.PersonPhotoUrlThumbnail = item.PhotoUrlThumbnail;
                model.PersonUsername = item.Username;

                int result = await db.PersonFollowing.CountAsync(x => x.PersonID == like.PersonID && x.SecondaryPersonID == item.PersonID && x.IsAccepted == true);
                if (result > 0)
                {
                    model.isFollowing = true;
                }
                likeModels.Add(model);
            }

            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.DateNow = DateTime.Now;
            cResponse.Data = likeModels;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
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