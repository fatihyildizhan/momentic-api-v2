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

                // get DeviceType and create new if not exist
                DeviceType dbDeviceType = await db.DeviceType.Where(x => x.Name == device.DeviceTypeName).SingleOrDefaultAsync();
                if (dbDeviceType == null)
                {
                    dbDeviceType = new DeviceType();
                    dbDeviceType.Name = device.DeviceTypeName;

                    db.DeviceType.Add(dbDeviceType);
                    await db.SaveChangesAsync();
                }

                // get OsVersion and create new if not exist
                OsVersion dbOsVersion = await db.OsVersion.Where(x => x.Name == device.OsVersionName).SingleOrDefaultAsync();
                if (dbOsVersion == null)
                {
                    dbOsVersion = new OsVersion();
                    dbOsVersion.Name = device.OsVersionName;

                    db.OsVersion.Add(dbOsVersion);
                    await db.SaveChangesAsync();
                }

                // get OsVersion and create new if not exist
                AppVersion dbAppVersion = await db.AppVersion.Where(x => x.Name == device.AppVersionName).SingleOrDefaultAsync();
                if (dbAppVersion == null)
                {
                    dbAppVersion = new AppVersion();
                    dbAppVersion.Name = device.AppVersionName;
                    dbAppVersion.DatePublish = device.AppVersionDatePublish;

                    db.AppVersion.Add(dbAppVersion);
                    await db.SaveChangesAsync();
                }

                Device isFoundDevice = await db.Device.Where(x => x.DeviceToken == device.DeviceToken && x.PersonID == device.PersonID && x.IsActive == true && x.DeviceTypeID == dbDeviceType.DeviceTypeID).SingleOrDefaultAsync();
                if (isFoundDevice != null)
                {
                    bool isChanged = false;
                    if (isFoundDevice.DeviceTypeID != dbDeviceType.DeviceTypeID)
                    {
                        isChanged = true;
                    }

                    if (isFoundDevice.OsVersionID != dbOsVersion.VersionID)
                    {
                        isChanged = true;
                    }

                    if (isFoundDevice.AppVersionID != dbAppVersion.VersionID)
                    {
                        isChanged = true;
                    }

                    if (isFoundDevice.DeviceOsID != device.DeviceOsID)
                    {
                        isChanged = true;
                    }

                    if (isFoundDevice.DeviceLanguageID != device.DeviceLanguageID)
                    {
                        isChanged = true;
                    }

                    if (isFoundDevice.DeviceLanguageID != device.DeviceLanguageID)
                    {
                        isChanged = true;
                    }

                    if (isFoundDevice.DeviceOsID != device.DeviceOsID)
                    {
                        isChanged = true;
                    }

                    if (isChanged)
                    {
                        isFoundDevice.IsActive = false;
                        await db.SaveChangesAsync();

                        await InsertNewDevice(device, dbDeviceType, dbOsVersion, dbAppVersion);
                    }
                    else
                    {
                        isFoundDevice.DateLastLogin = DateTime.Now;
                        await db.SaveChangesAsync();
                    }

                    cResponse.Result = "0";
                    cResponse.Description = "Device Updated";
                    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cResponse));
                }
                else
                {
                    await InsertNewDevice(device, dbDeviceType, dbOsVersion, dbAppVersion);

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

        private async Task InsertNewDevice(DeviceNameModel device, DeviceType dbDeviceType, OsVersion dbOsVersion, AppVersion dbAppVersion)
        {
            Device dvc = new Device();

            // Insert to table first then get ID if not exists
            dvc.DeviceTypeID = dbDeviceType.DeviceTypeID;
            dvc.OsVersionID = dbOsVersion.VersionID;
            dvc.AppVersionID = dbAppVersion.VersionID;
            dvc.DeviceToken = device.DeviceToken;
            dvc.IsActive = true;
            dvc.DateLogin = DateTime.Now;
            dvc.DateLastLogin = DateTime.Now;
            dvc.PersonID = device.PersonID;
            dvc.DeviceOsID = device.DeviceOsID;
            dvc.DeviceLanguageID = device.DeviceLanguageID;
            db.Device.Add(dvc);

            await db.SaveChangesAsync();
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