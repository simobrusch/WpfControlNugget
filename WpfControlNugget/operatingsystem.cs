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
    
    public partial class operatingsystem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public operatingsystem()
        {
            this.devicetype_has_operatingsystem = new HashSet<devicetype_has_operatingsystem>();
        }
    
        public long operatingsystem_id { get; set; }
        public string operatingsystemName { get; set; }
        public string model { get; set; }
        public string version { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<devicetype_has_operatingsystem> devicetype_has_operatingsystem { get; set; }
    }
}
