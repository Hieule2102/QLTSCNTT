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
    
    public partial class THIETBI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public THIETBI()
        {
            this.CAU_HINH = new HashSet<CAU_HINH>();
            this.DIEU_CHUYEN_THIET_BI = new HashSet<DIEU_CHUYEN_THIET_BI>();
            this.HINH_ANH = new HashSet<HINH_ANH>();
            this.NHAP_KHO = new HashSet<NHAP_KHO>();
            this.THONG_KE_THIETBI = new HashSet<THONG_KE_THIETBI>();
            this.XUAT_KHO = new HashSet<XUAT_KHO>();
        }
    
        public int MATB { get; set; }
        public string TENTB { get; set; }
        public string SO_SERIAL { get; set; }
        public Nullable<decimal> GIA_TIEN { get; set; }
        public string THOI_HAN_BAO_HANH { get; set; }
        public string TINH_TRANG { get; set; }
        public string MA_LOAITB { get; set; }
        public string MANS_QL { get; set; }
        public Nullable<int> MA_DV { get; set; }
        public Nullable<int> MA_NCC { get; set; }
        public Nullable<System.DateTime> NGAY_GD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAU_HINH> CAU_HINH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIEU_CHUYEN_THIET_BI> DIEU_CHUYEN_THIET_BI { get; set; }
        public virtual DON_VI DON_VI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINH_ANH> HINH_ANH { get; set; }
        public virtual LOAI_THIETBI LOAI_THIETBI { get; set; }
        public virtual NHA_CUNG_CAP NHA_CUNG_CAP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAP_KHO> NHAP_KHO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THONG_KE_THIETBI> THONG_KE_THIETBI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XUAT_KHO> XUAT_KHO { get; set; }
    }
}
