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
    
    public partial class CountStory
    {
        public int StoryID { get; set; }
        public int Comment { get; set; }
        public int Moment { get; set; }
        public int ReTell { get; set; }
        public int Report { get; set; }
        public int Tag { get; set; }
        public int Location { get; set; }
        public System.DateTime LastActivityDate { get; set; }
        public int MomentHorizontal { get; set; }
        public int MomentSquare { get; set; }
    
        public virtual Story Story { get; set; }
    }
}