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
    
    public partial class softwaredienstleistung
    {
        public int software_id { get; set; }
        public int stundenaufwand { get; set; }
        public long abrechung_fk { get; set; }
    
        public virtual abrechnung abrechnung { get; set; }
    }
}
