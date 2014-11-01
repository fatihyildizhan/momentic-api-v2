using MomenticAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Dynamic;
using Newtonsoft.Json;
using System;

namespace MomenticAPI.Controllers
{
    public class PersonLoginController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/Person
        [ResponseType(typeof(Person))]
        public async Task<object> PostPerson(Person personLogin)
        {
            string email = null;
            string username = null;

            if (personLogin.Username.IndexOf("@") == -1)
            {
                username = personLogin.Username;
            }
            else
            {
                email = personLogin.Username;
            }

            Person person;
            if (email != null)
            {
                person = await db.Person.Where(x => x.Email == email && x.Password == personLogin.Password).FirstOrDefaultAsync();
            }
            else
            {
                person = await db.Person.Where(x => x.Username == username && x.Password == personLogin.Password).FirstOrDefaultAsync();
            }

            dynamic cResponse = new ExpandoObject();
            if (person == null)
            {
                cResponse.Result = "-1";
                cResponse.Message = "Person Not Found";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            string tokenResult = BasicHelper.TokenCreate(person.PersonID);

            if (tokenResult == "-1")
            {
                cResponse.Result = "-1";
                cResponse.Message = "Token could not created";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }

            person.LastLoginDate = DateTime.Now;
            await db.SaveChangesAsync();

            cResponse.Result = "0";
            cResponse.Token = tokenResult;
            cResponse.Person = person;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }


        // together
        //public object PostPerson(PersonLoginDevice personLogin)
        //{
        //    Person person;
        //    if (personLogin.Email != null)
        //    {
        //        person = db.Person.Where(x => x.Email == personLogin.Email && x.Password == personLogin.Password).FirstOrDefault();
        //    }
        //    else
        //    {
        //        person = db.Person.Where(x => x.Username == personLogin.Username && x.Password == personLogin.Password).FirstOrDefault();
        //    }

        //    dynamic cResponse = new ExpandoObject();
        //    if (person == null)
        //    {
        //        cResponse.Result = "-1";
        //        cResponse.Message = "Person Not Found";
        //        return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        //    }

        //    AddDevice(personLogin, person.PersonID);

        //     person.LastLoginDate = DateTime.Now;
        //     db.SaveChanges();


        //     personLogin.PersonID = person.PersonID;
        //     personLogin.Email = person.Email;
        //     personLogin.Username = person.Username;

        //    cResponse.Result = "1";
        //    cResponse.Person = personLogin;
        //    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        //}

        //private void AddDevice(PersonLoginDevice personLogin, int personID)
        //{
        //    Device isFoundDevice = db.Device.Where(x => x.DeviceToken == personLogin.DeviceToken && x.PersonID == personID && x.DeviceTypeID == personLogin.DeviceTypeID).SingleOrDefault();

        //    if (isFoundDevice != null)
        //    {
        //        isFoundDevice.LoginDate = DateTime.Now;
        //        isFoundDevice.IsActive = true;
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        Device newDevice = new Device();
        //        newDevice.LoginDate = DateTime.Now;
        //        newDevice.IsActive = true;
        //        newDevice.PersonID = personID;
        //        newDevice.OsVersion = personLogin.OsVersion;
        //        newDevice.DeviceTypeID = personLogin.DeviceTypeID;
        //        newDevice.DeviceToken = personLogin.DeviceToken;
        //        db.Device.Add(newDevice);
        //        db.SaveChanges();
        //    }
        //}



        /*
         // GET: api/Person
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> PostPerson(Person personLogin)
        {
            Person person;
            if (personLogin.Email != null)
            {
                person = await db.Person.Where(x => x.Email == personLogin.Email && x.Password == personLogin.Password).FirstOrDefaultAsync();
            }
            else
            {
                person = await db.Person.Where(x => x.Username == personLogin.Username && x.Password == personLogin.Password).FirstOrDefaultAsync();
            }
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
         */
    }
}
