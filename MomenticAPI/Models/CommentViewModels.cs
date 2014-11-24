using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomenticAPI.Models
{
    public class CommentViewModel
    {
        public int CommentID { get; set; }
        public int PersonID { get; set; }
        public string PersonThumbnail { get; set; }
        public string PersonUsername { get; set; }
        public int StoryID { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
    }
}