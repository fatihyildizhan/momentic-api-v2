using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomenticAPI.Models
{
    public class DeviceNameModel
    {
        public string DeviceToken { get; set; }
        public string DeviceName { get; set; }
        public string OsVersion { get; set; }
        public int PersonID { get; set; }
        public int DeviceOSID { get; set; }
        public int DeviceLanguageID { get; set; }
        public int AppVersionID { get; set; }
    }
}