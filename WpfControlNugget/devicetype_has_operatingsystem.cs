//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfControlNugget
{
    using System;
    using System.Collections.Generic;
    
    public partial class devicetype_has_operatingsystem
    {
        public long deviceType_fk { get; set; }
        public long operatingsystem_fk { get; set; }
    
        public virtual operatingsystem operatingsystem { get; set; }
    }
}
