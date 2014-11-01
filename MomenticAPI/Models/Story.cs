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
    
    public partial class Story
    {
        public Story()
        {
            this.Comment = new HashSet<Comment>();
            this.Report = new HashSet<Report>();
            this.Timeline = new HashSet<Timeline>();
        }
    
        public int StoryID { get; set; }
        public int StoryCategoryID { get; set; }
        public int ThemeID { get; set; }
        public int PersonID { get; set; }
        public string Tag { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsReported { get; set; }
        public bool IsPrivate { get; set; }
        public int MomentID1 { get; set; }
        public int MomentID2 { get; set; }
        public Nullable<int> MomentID3 { get; set; }
        public Nullable<int> MomentID4 { get; set; }
        public Nullable<int> MomentID5 { get; set; }
        public Nullable<int> MomentID6 { get; set; }
        public Nullable<int> MomentID7 { get; set; }
    
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual Moment Moment { get; set; }
        public virtual Moment Moment1 { get; set; }
        public virtual Moment Moment2 { get; set; }
        public virtual Moment Moment3 { get; set; }
        public virtual Moment Moment4 { get; set; }
        public virtual Moment Moment5 { get; set; }
        public virtual Moment Moment6 { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual StoryCategory StoryCategory { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual ICollection<Timeline> Timeline { get; set; }
    }
}
