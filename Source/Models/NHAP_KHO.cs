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
    
    public partial class NHAP_KHO
    {
        public int MA_NHAPKHO { get; set; }
        public int MATB { get; set; }
        public Nullable<int> MADV_NHAP { get; set; }
        public string MANS_NHAP { get; set; }
        public Nullable<System.DateTime> NGAY_NHAP { get; set; }
    
        public virtual DON_VI DON_VI { get; set; }
        public virtual NGUOI_DUNG NGUOI_DUNG { get; set; }
        public virtual THIETBI THIETBI { get; set; }
    }
}
