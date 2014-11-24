using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomenticAPI.Models
{
    public class StoryViewModel
    {
        public int StoryID { get; set; }
        public int ThemeID { get; set; }
        public string Tag { get; set; }

        public List<MomentViewModel> MomentList { get; set; }
     //   public List<CommentViewModel> CommentList { get; set; }
     //   List<MomentLikeViewModel> MomentLikeList { get; set; }
    }
}