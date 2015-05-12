using MomenticAPI.Models;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace MomenticAPI.Controllers
{
    public class CheckEmailController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        [ResponseType(typeof(PersonEmail))]
        public async Task<object> PostPersonEmail(PersonEmail checkEmail)
        {
            bool flag = isEmail(checkEmail.Email);

            dynamic cResponse = new ExpandoObject();

            if (!String.IsNullOrEmpty(checkEmail.Email) && flag)
            {
                int count = await db.Person.CountAsync(x => x.Email == checkEmail.Email);

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
            else
            {
                cResponse.Result = "-1";
                cResponse.Description = "Email format is wrong";
                cResponse.Email = checkEmail.Email;
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        private bool isEmail(string inputEmail)
        {
            Regex re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",
                          RegexOptions.IgnoreCase);
            return re.IsMatch(inputEmail);
        }
    }
}
