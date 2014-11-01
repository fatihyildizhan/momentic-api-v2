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
    public class DeviceTypeController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/DeviceType
        public object GetDeviceType()
        {
            dynamic cResponse = new ExpandoObject();
            cResponse.Result = "0";
            cResponse.Person = db.DeviceType;

            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/DeviceType/5
        [ResponseType(typeof(DeviceType))]
        public async Task<IHttpActionResult> GetDeviceType(int id)
        {
            DeviceType deviceType = await db.DeviceType.FindAsync(id);
            if (deviceType == null)
            {
                return NotFound();
            }

            return Ok(deviceType);
        }

        // PUT: api/DeviceType/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDeviceType(int id, DeviceType deviceType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deviceType.DeviceTypeID)
            {
                return BadRequest();
            }

            db.Entry(deviceType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceTypeExists(id))
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

        // POST: api/DeviceType
        [ResponseType(typeof(DeviceType))]
        public async Task<IHttpActionResult> PostDeviceType(DeviceType deviceType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeviceType.Add(deviceType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = deviceType.DeviceTypeID }, deviceType);
        }

        // DELETE: api/DeviceType/5
        [ResponseType(typeof(DeviceType))]
        public async Task<IHttpActionResult> DeleteDeviceType(int id)
        {
            DeviceType deviceType = await db.DeviceType.FindAsync(id);
            if (deviceType == null)
            {
                return NotFound();
            }

            db.DeviceType.Remove(deviceType);
            await db.SaveChangesAsync();

            return Ok(deviceType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceTypeExists(int id)
        {
            return db.DeviceType.Count(e => e.DeviceTypeID == id) > 0;
        }
    }
}