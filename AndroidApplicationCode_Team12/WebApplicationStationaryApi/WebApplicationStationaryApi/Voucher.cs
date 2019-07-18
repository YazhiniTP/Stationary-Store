//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplicationStationaryApi
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class Voucher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voucher()
        {
            this.VoucherDetails = new HashSet<VoucherDetail>();
        }
    
        public int VoucherID { get; set; }
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
    }
}
