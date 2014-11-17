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
    
    public partial class Person
    {
        public Person()
        {
            this.Activity = new HashSet<Activity>();
            this.Activity1 = new HashSet<Activity>();
            this.Comment = new HashSet<Comment>();
            this.Device = new HashSet<Device>();
            this.Feedback = new HashSet<Feedback>();
            this.Moment = new HashSet<Moment>();
            this.MomentLike = new HashSet<MomentLike>();
            this.Notification = new HashSet<Notification>();
            this.PersonFollowing = new HashSet<PersonFollowing>();
            this.PersonFollowing1 = new HashSet<PersonFollowing>();
            this.PersonToken = new HashSet<PersonToken>();
            this.Report = new HashSet<Report>();
            this.ReTell = new HashSet<ReTell>();
            this.SearchHistory = new HashSet<SearchHistory>();
            this.Story = new HashSet<Story>();
            this.Timeline = new HashSet<Timeline>();
            this.Theme = new HashSet<Theme>();
        }
    
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoUrlThumbnail { get; set; }
        public string PhotoUrlLarge { get; set; }
        public string PhotoUrlOriginal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> Code { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public int GenderID { get; set; }
        public int PersonRoleID { get; set; }
        public System.DateTime LastLoginDate { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public bool IsPrivate { get; set; }
        public string About { get; set; }
        public bool IsPushAllowed { get; set; }
        public bool IsEmailAllowed { get; set; }
        public bool IsSuspended { get; set; }
    
        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<Activity> Activity1 { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual CountPerson CountPerson { get; set; }
        public virtual ICollection<Device> Device { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<Moment> Moment { get; set; }
        public virtual ICollection<MomentLike> MomentLike { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual PersonRole PersonRole { get; set; }
        public virtual ICollection<PersonFollowing> PersonFollowing { get; set; }
        public virtual ICollection<PersonFollowing> PersonFollowing1 { get; set; }
        public virtual ICollection<PersonToken> PersonToken { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual ICollection<ReTell> ReTell { get; set; }
        public virtual ICollection<SearchHistory> SearchHistory { get; set; }
        public virtual ICollection<Story> Story { get; set; }
        public virtual ICollection<Timeline> Timeline { get; set; }
        public virtual ICollection<Theme> Theme { get; set; }
    }
}
