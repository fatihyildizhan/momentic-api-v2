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
    
    public partial class DeviceType
    {
        public DeviceType()
        {
            this.Device = new HashSet<Device>();
        }
    
        public int DeviceTypeID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Device> Device { get; set; }
    }
}
