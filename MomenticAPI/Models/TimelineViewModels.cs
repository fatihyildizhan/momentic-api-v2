using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomenticAPI.Models
{
    public class TimelineViewModel
    {
        public int TimelineID { get; set; }
        public int StoryID { get; set; }
        public int ThemeID { get; set; }
        public string Tag { get; set; }
        public int PersonID { get; set; }
        public string PersonThumbnail { get; set; }
        public string PersonUsername { get; set; }
        public DateTime FeedDate { get; set; }
        public bool IsReTell { get; set; }
        public int CoverPhotoIndex { get; set; }
        public string PhotoUrlLarge { get; set; }
        public bool IsHorizontal { get; set; }
        public string LocationString { get; set; }
        public string Title { get; set; }
    }
}


