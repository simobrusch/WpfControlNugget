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
    
    public partial class contact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public contact()
        {
            this.pointofdeliveries = new HashSet<pointofdelivery>();
        }
    
        public long contact_id { get; set; }
        public long person_fk { get; set; }
        public string priority { get; set; }
    
        public virtual person person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pointofdelivery> pointofdeliveries { get; set; }
    }
}
