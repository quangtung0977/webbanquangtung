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
    
    public partial class loaisanpham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public loaisanpham()
        {
            this.sanphams = new HashSet<sanpham>();
        }
    
        public int id { get; set; }
        public string ten { get; set; }
        public Nullable<System.DateTime> ngaytao { get; set; }
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        public Nullable<int> nguoitao { get; set; }
        public bool daxoa { get; set; }
        public string hinhanhloaisanpham { get; set; }
        public string motasoluoc { get; set; }
        public string ghichu { get; set; }
        public bool hienthi { get; set; }
        public int thutuhien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sanpham> sanphams { get; set; }
    }
}
