//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class admin_thanhvien
    {
        public int id { get; set; }
        public string tenthanhvien { get; set; }
        public string motasoluoc { get; set; }
        public string chitiet { get; set; }
        public string urlhinhanh { get; set; }
        public Nullable<System.DateTime> ngaytao { get; set; }
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        public Nullable<int> nguoitao { get; set; }
        public Nullable<bool> daxoa { get; set; }
        public string ghichu { get; set; }
        public Nullable<int> nhomthanhvien { get; set; }
        public Nullable<bool> hienthi { get; set; }
        public string tenthanhvien_en { get; set; }
        public string motasoluoc_en { get; set; }
        public string chitiet_en { get; set; }
        public Nullable<int> thutuhienthi { get; set; }
    
        public virtual admin_account admin_account { get; set; }
        public virtual admin_nhomthanhvien admin_nhomthanhvien { get; set; }
    }
}
