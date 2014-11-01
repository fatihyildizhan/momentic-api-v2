using MomenticAPI.Models;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;


namespace MomenticAPI.Controllers
{
    public class CheckEmailController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        [ResponseType(typeof(PersonEmail))]
        public async Task<object> PostPersonEmail(PersonEmail checkEmail)
        {

            int count = await db.Person.CountAsync(x => x.Email == checkEmail.Email);

            dynamic cResponse = new ExpandoObject();
            if (count > 0)
            {
                cResponse.Result = "-1";
                cResponse.Description = "Email is not available";
                cResponse.Email = checkEmail.Email;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
            else
            {
                cResponse.Result = "0";
                cResponse.Description = "Email is available";
                cResponse.Email = checkEmail.Email;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }
    }
}
