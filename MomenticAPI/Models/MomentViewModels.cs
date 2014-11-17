using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomenticAPI.Models
{
    public class MomentViewModel
    {
        public int MomentID { get; set; }
        public string PhotoUrlLarge { get; set; }
        public int PersonID { get; set; }
        public string Title { get; set; }
        public bool IsHorizontal { get; set; }
        public string LocationString { get; set; }
    }
}