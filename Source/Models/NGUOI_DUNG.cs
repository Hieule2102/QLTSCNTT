//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Source.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NGUOI_DUNG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGUOI_DUNG()
        {
            this.NHAP_KHO = new HashSet<NHAP_KHO>();
            this.NHOM_ND = new HashSet<NHOM_ND>();
            this.XUAT_KHO = new HashSet<XUAT_KHO>();
            this.XUAT_KHO1 = new HashSet<XUAT_KHO>();
        }
    
        public string MA_ND { get; set; }
        public string TEN_ND { get; set; }
        public Nullable<int> MA_DON_VI { get; set; }
        public string EMAIL { get; set; }
        public string TEN_DANG_NHAP { get; set; }
        public string MAT_KHAU { get; set; }
        public Nullable<System.DateTime> LAN_CUOI_DANG_NHAP { get; set; }
        public Nullable<int> SO_LAN_SAI_MAT_KHAU { get; set; }
        public Nullable<bool> KHOA_TAI_KHOAN { get; set; }
    
        public virtual DON_VI DON_VI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAP_KHO> NHAP_KHO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHOM_ND> NHOM_ND { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XUAT_KHO> XUAT_KHO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XUAT_KHO> XUAT_KHO1 { get; set; }
    }
}
