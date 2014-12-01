using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomenticAPI.Models
{
    public class DeviceNameModel
    {
        public string DeviceToken { get; set; }
        public int PersonID { get; set; }
        public int DeviceOsID { get; set; }
        public DateTime AppVersionDatePublish { get; set; }
        public string AppVersionName { get; set; } // Create AppVersionID
        public string DeviceTypeName { get; set; } // Create DeviceTypeID
        public string OsVersionName { get; set; } // Create OsVersionID
        public string DeviceLanguageName { get; set; } // Create LanguageID
    }
}