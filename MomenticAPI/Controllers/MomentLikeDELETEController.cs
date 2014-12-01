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

    public class MomentLikeDELETEController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        public async Task<object> PostMomentLike(MomentLike like)
        {
            dynamic cResponse = new ExpandoObject();
            try
            {
                MomentLike mLike = await db.MomentLike.Where(x => x.MomentID == like.MomentID && x.PersonID == like.PersonID).SingleOrDefaultAsync();
                if (mLike != null)
                {
                    db.MomentLike.Remove(mLike);
                    await db.SaveChangesAsync();

                    CountMoment mMoment = await db.CountMoment.FindAsync(like.MomentID);
                    if (mMoment != null)
                    {
                        mMoment.LastActivityDate = DateTime.Now;
                        mMoment.LikeCount = mMoment.LikeCount - 1;
                        await db.SaveChangesAsync();
                    }

                    cResponse.Result = "0";
                    cResponse.Description = "you unliked";
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }
                else
                {
                    cResponse.Result = "0";
                    cResponse.Description = "you unliked";
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }
            }
            catch (Exception ex)
            {
                cResponse.Result = "0";
                cResponse.Description = "Exception, your request could not be executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }
    }
}
