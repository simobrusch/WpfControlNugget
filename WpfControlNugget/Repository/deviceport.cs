//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfControlNugget.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class deviceport
    {
        public int deviceport_id { get; set; }
        public string description { get; set; }
        public long device_fk { get; set; }
        public long transportmedium_fk { get; set; }
    
        public virtual device device { get; set; }
        public virtual transportmedium transportmedium { get; set; }
    }
}
