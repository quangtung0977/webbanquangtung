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
    
    public partial class sanpham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sanpham()
        {
            this.chitietdonhangs = new HashSet<chitietdonhang>();
        }
    
        public string id { get; set; }
        public string ten { get; set; }
        public Nullable<int> loaisanpham { get; set; }
        public Nullable<System.DateTime> ngaytao { get; set; }
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        public Nullable<int> nguoitao { get; set; }
        public string motasoluoc { get; set; }
        public string motachitiet { get; set; }
        public Nullable<bool> daxoa { get; set; }
        public string ghichu { get; set; }
        public Nullable<bool> hienthi { get; set; }
        public string hinhdaidien { get; set; }
        public string hinhanhsanpham { get; set; }
        public Nullable<int> thutuhien { get; set; }
        public Nullable<int> gianiemyet { get; set; }
        public Nullable<int> giaban { get; set; }
        public Nullable<int> mathuonghieu { get; set; }
    
        public virtual loaisanpham loaisanpham1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietdonhang> chitietdonhangs { get; set; }
    }
}
