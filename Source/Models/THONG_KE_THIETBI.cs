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
    
    public partial class THONG_KE_THIETBI
    {
        public int MA_THONG_KE { get; set; }
        public Nullable<int> MATB { get; set; }
        public string TRANG_THAI { get; set; }
        public Nullable<int> MANS_QL { get; set; }
    
        public virtual THIETBI THIETBI { get; set; }
    }
}
