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
    
    public partial class @interface
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public @interface()
        {
            this.abrechnungs = new HashSet<abrechnung>();
        }
    
        public long interface_id { get; set; }
        public int network_fk { get; set; }
        public long device_fk { get; set; }
        public string ip_adress_v4 { get; set; }
        public string mac_adresse { get; set; }
        public byte[] isFullDuplex { get; set; }
        public Nullable<int> bandwith { get; set; }
        public Nullable<short> is_in_use { get; set; }
        public string description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<abrechnung> abrechnungs { get; set; }
        public virtual device device { get; set; }
        public virtual network network { get; set; }
    }
}