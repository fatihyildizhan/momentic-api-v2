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
using System.Web.Mvc;

namespace MomenticAPI.Controllers
{
    [AuthorizationKeyFilterAttribute("Token")]
    public class DeviceController : ApiController
    {
        private MomenticEntities db = new MomenticEntities();

        // GET: api/Device
        [OutputCache(Duration = 3600, VaryByParam = "*")]
        public object GetDevice()
        {
            dynamic cResponse = new ExpandoObject();

            cResponse.Result = "0";
            cResponse.Device = db.Device;
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
        }

        // GET: api/Device/5
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> GetDevice(string id)
        {
            Device device = await db.Device.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // PUT: api/Device/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDevice(string id, Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.DeviceToken)
            {
                return BadRequest();
            }

            db.Entry(device).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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

        // POST: api/Device
        [ResponseType(typeof(DeviceNameModel))]
        public async Task<object> PostDevice(DeviceNameModel device)
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

                DeviceType dt = db.DeviceType.Where(x => x.DeviceTypeName == device.DeviceName).SingleOrDefault();
                if (dt == null)
                {
                    dt = new DeviceType();
                    dt.DeviceTypeName = device.DeviceName;

                    db.DeviceType.Add(dt);
                    db.SaveChanges();
                }

                Device isFoundDevice = await db.Device.Where(x => x.DeviceToken == device.DeviceToken && x.DeviceTypeID == dt.DeviceTypeID && x.PersonID == device.PersonID).SingleOrDefaultAsync();
                if (isFoundDevice != null)
                {
                    isFoundDevice.LastLoginDate = DateTime.Now;
                    await db.SaveChangesAsync();

                    cResponse.Result = "0";
                    cResponse.Description = "Last login date updated";
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }
                else
                {
                    Device dvc = new Device();
                    dvc.DeviceToken = device.DeviceToken;
                    dvc.DeviceTypeID = dt.DeviceTypeID;
                    dvc.IsActive = true;
                    dvc.LoginDate = DateTime.Now;
                    dvc.LastLoginDate = DateTime.Now;
                    dvc.OsVersion = device.OsVersion;
                    dvc.PersonID = device.PersonID;
                    dvc.DeviceOSID = device.DeviceOSID;
                    dvc.DeviceLanguageID = device.DeviceLanguageID;
                    dvc.AppVersionID = device.AppVersionID;
                    db.Device.Add(dvc);

                    await db.SaveChangesAsync();

                    cResponse.Result = "0";
                    cResponse.Description = "Device added to database";
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }
            }
            catch
            {
                cResponse.Result = "-1";
                cResponse.Description = "Exception, your request could not be executed";
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
            }
        }

        // DELETE: api/Device/5
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> DeleteDevice(string id)
        {
            Device device = await db.Device.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            db.Device.Remove(device);
            await db.SaveChangesAsync();

            return Ok(device);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceExists(string id)
        {
            return db.Device.Count(e => e.DeviceToken == id) > 0;
        }
    }
}