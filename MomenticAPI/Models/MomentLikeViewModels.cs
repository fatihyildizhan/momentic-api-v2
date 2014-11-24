using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomenticAPI.Models
{
    public class MomentLikeViewModel
    {
        public int MomentID { get; set; }
        public int PersonID { get; set; }
        public string PersonUsername { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public string PersonPhotoUrlThumbnail { get; set; }
        public bool isFollowing { get; set; }
    }
}