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
    
    public partial class CountMoment
    {
        public int MomentID { get; set; }
        public System.DateTime LastActivityDate { get; set; }
        public int LikeCount { get; set; }
        public int ReTellCover { get; set; }
    
        public virtual Moment Moment { get; set; }
    }
}