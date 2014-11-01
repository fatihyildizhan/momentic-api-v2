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
    
    public partial class Notification
    {
        public int NotificationID { get; set; }
        public int NotificationCaseID { get; set; }
        public int NotificationTypeID { get; set; }
        public int PersonID { get; set; }
        public System.DateTime RequestDate { get; set; }
        public Nullable<System.DateTime> SentDate { get; set; }
        public bool IsSent { get; set; }
    
        public virtual NotificationCase NotificationCase { get; set; }
        public virtual NotificationType NotificationType { get; set; }
        public virtual Person Person { get; set; }
    }
}