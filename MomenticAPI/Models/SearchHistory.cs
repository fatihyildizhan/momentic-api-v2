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
    
    public partial class SearchHistory
    {
        public int SearchID { get; set; }
        public int PersonID { get; set; }
        public System.DateTime SearchDate { get; set; }
        public string Text { get; set; }
        public int CountResult { get; set; }
    
        public virtual Person Person { get; set; }
    }
}
