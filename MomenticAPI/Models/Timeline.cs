//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MomenticAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Timeline
    {
        public int TimelineID { get; set; }
        public int PersonID { get; set; }
        public int StoryID { get; set; }
        public System.DateTime FeedDate { get; set; }
        public bool IsReTell { get; set; }
        public int CoverPhotoIndex { get; set; }
        public int ThemeID { get; set; }
        public string Tag { get; set; }
        public string PersonThumbnail { get; set; }
        public string PersonUsername { get; set; }
        public string PhotoUrlLarge { get; set; }
        public bool IsHorizontal { get; set; }
        public string LocationString { get; set; }
        public string Title { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual Story Story { get; set; }
        public virtual Theme Theme { get; set; }
    }
}
