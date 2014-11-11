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
    public class CheckUsernameController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        [ResponseType(typeof(PersonUsername))]
        public async Task<object> PostPersonUsername(PersonUsername checkUsername)
        {
            dynamic cResponse = new ExpandoObject();

            if (!String.IsNullOrEmpty(checkUsername.Username))
            {
                int count = await db.Person.CountAsync(x => x.Username == checkUsername.Username);

                if (count > 0)
                {
                    cResponse.Result = "-1";
                    cResponse.Description = "Username is not available";
                    cResponse.Username = checkUsername.Username;
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }
                else
                {
                    cResponse.Result = "0";
                    cResponse.Description = "Username is available";
                    cResponse.Username = checkUsername.Username;
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }
            }
            else
            {
                cResponse.Result = "-1";
                cResponse.Description = "Username format is wrong";
                cResponse.Username = checkUsername.Username;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }
    }
}
