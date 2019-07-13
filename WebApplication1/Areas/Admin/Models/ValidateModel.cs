using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Areas.Admin.Models
{
    // 
    // Hành trình
    //
    #region Hành trình
    [MetadataType(typeof(admin_hanhtrinhMetaData))]
    public partial class admin_hanhtrinh
    { }
    public partial class admin_hanhtrinhMetaData
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Nhập thiếu thông tin.")]
        [Display(Name = "Tên hành trình")]
        //[StringLength(20, MinimumLength = 5, ErrorMessage = "Độ dài từ 5 -> 20 kí tự.")]
        //[RegularExpression(@"^\S*$", ErrorMessage = "Trường không được chứa khoảng trắng.")]
        public string tenhanhtrinh { get; set; }
        [Display(Name = "Loại hành trình")]
        public Nullable<int> loaihanhtrinh { get; set; }
        [Display(Name = "Thời điểm chuyến đi")]
        [Required(ErrorMessage = "Nhập thiếu thông tin.")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        [Display(Name = "Mô tả sơ lược")]
        [Required(ErrorMessage = "Nhập thiếu thông tin.")]
        public string motasoluoc { get; set; }
        [Display(Name = "Nội dung")]
        public string noidung { get; set; }
        [Display(Name = "Hiển thị")]
        public Nullable<bool> hienthi { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<int> nguoitao { get; set; }
        public Nullable<bool> daxoa { get; set; }
        [Display(Name = "Ghi chú")]
        public string ghichu { get; set; }
        [Display(Name = "Thời gian")]
        public string thongtinthoigian { get; set; }
        [Display(Name = "Địa điểm")]
        public string thongtindiadiem { get; set; }
        public string diadiem_en { get; set; }
        [Display(Name = "Hìnn đại diện")]
        public string hinhdaidien { get; set; }
        public string tenhanhtrinh_en { get; set; }
        public string motasoluoc_en { get; set; }
        public string noidung_en { get; set; }
    }
    #endregion

    // 
    // Loại hành trình
    //
    #region Loại hành trình
    [MetadataType(typeof(admin_loaihanhtrinhMetaData))]
    public partial class admin_loaihanhtrinh
    { }
    public partial class admin_loaihanhtrinhMetaData
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Nhập thiếu thông tin.")]
        [Display(Name = "Tên loại hành trình")]
        //[StringLength(20, MinimumLength = 5, ErrorMessage = "Độ dài từ 5 -> 20 kí tự.")]
        //[RegularExpression(@"^\S*$", ErrorMessage = "Trường không được chứa khoảng trắng.")]
        public string tenloaihanhtrinh { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        [Display(Name = "Hiển thị")]
        public Nullable<bool> hienthi { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<int> nguoitao { get; set; }
        public virtual admin_account admin_account { get; set; }
        public virtual ICollection<admin_hanhtrinh> admin_hanhtrinh { get; set; }
    }
    #endregion

    // 
    // Account
    //
    #region Account
    [MetadataType(typeof(admin_accountMetaData))]
    public partial class admin_account
    { }
    public partial class admin_accountMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên user")]
        public string username { get; set; }
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Lần đăng nhập cuối")]
        public Nullable<System.DateTime> landangnhapcuoi { get; set; }
        [Display(Name = "Họ tên")]
        public string hoten { get; set; }
        [Display(Name = "Giới tính")]
        public Nullable<bool> gioitinh { get; set; }
        [Display(Name = "Ghi chú")]
        public string ghichu { get; set; }
        [Display(Name = "Quyền")]
        public Nullable<int> quyen { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<admin_hanhtrinh> admin_hanhtrinh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<admin_loaihanhtrinh> admin_loaihanhtrinh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<admin_hinhanh> admin_hinhanh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<admin_loaihinhanh> admin_loaihinhanh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<admin_nhomthanhvien> admin_nhomthanhvien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<banner> banners { get; set; }
    }
    #endregion

    // 
    // Hình ảnh
    //
    #region Hình ảnh
    [MetadataType(typeof(admin_hinhanhMetaData))]
    public partial class admin_hinhanh
    { }
    public partial class admin_hinhanhMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên hình ảnh")]
        public string tenhinhanh { get; set; }
        [Display(Name = "Đường dẫn hình ảnh")]
        public string urlhinhanh { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<int> nguoitao { get; set; }
        [Display(Name = "Loại hình ảnh")]
        public Nullable<int> loaihinhanh { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }
        [Display(Name = "Ghi chú")]
        public string ghichu { get; set; }
        public Nullable<int> thutuhien { get; set; }
        //public string tenloaihinhanh_en { get; set; }
        //public string motahinhanh_en { get; set; }
        public virtual admin_account admin_account { get; set; }
        public virtual admin_loaihinhanh admin_loaihinhanh { get; set; }
    }
    #endregion

    // 
    // Loại hình ảnh
    //
    #region Loại hình ảnh
    [MetadataType(typeof(admin_loaihinhanhMetaData))]
    public partial class admin_loaihinhanh
    { }
    public partial class admin_loaihinhanhMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên chủ đề hình ảnh")]
        public string tenloaihinhanh { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<int> nguoitao { get; set; }
        public Nullable<bool> hienthi { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }
        [Display(Name = "Mô tả sơ lược")]
        public string motahinhanh { get; set; }
        [Display(Name = "Hình đại diện")]
        public string ghichu { get; set; }
        public string tenloaihinhanh_en { get; set; }
        public string motahinhanh_en { get; set; }

        public virtual admin_account admin_account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<admin_hinhanh> admin_hinhanh { get; set; }
    }
    #endregion

    // 
    // Tin tức
    //
    #region Tin tức
    [MetadataType(typeof(admin_tintucMetaData))]
    public partial class admin_tintuc
    { }
    public partial class admin_tintucMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên tin tức")]
        public string tentintuc { get; set; }
        [Display(Name = "Loại tin tức")]
        public Nullable<int> loaitintuc { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày đăng")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ngaycapnhat { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<System.DateTime> nguoitao { get; set; }
        [Display(Name = "Mô tả sơ lược")]
        public string motasoluoc { get; set; }
        [Display(Name = "Nội dung tin tức")]
        public string noidungtintuc { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }
        [Display(Name = "Ghi chú")]
        public string ghichu { get; set; }
        [Display(Name = "Hìnn đại diện")]
        public string hinhdaidien { get; set; }
        public string tentintuc_en { get; set; }
        public string noidungtintuc_en { get; set; }
        public virtual admin_account admin_account { get; set; }
        public virtual admin_loaitintuc admin_loaitintuc { get; set; }
    }
    #endregion

    // 
    // Loại tin tức
    //
    #region Loại tin tức
    [MetadataType(typeof(admin_loaitintucMetaData))]
    public partial class admin_loaitintuc
    { }
    public partial class admin_loaitintucMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên loại tin tức")]
        public string tenloaitintuc { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<int> nguoitao { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }
        [Display(Name = "Hình ảnh tin tức")]
        public string hinhanhtintuc { get; set; }
        [Display(Name = "Mô tả sơ lược")]
        public string motasoluoc { get; set; }
        [Display(Name = "Ghi chú")]
        public string ghichu { get; set; }
        public virtual ICollection<admin_hanhtrinh> admin_tintuc { get; set; }
    }
    #endregion

    // 
    // Thành viên
    //
    #region Thành viên
    [MetadataType(typeof(admin_thanhvienMetaData))]
    public partial class admin_thanhvien
    { }
    public partial class admin_thanhvienMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên thành viên")]
        public string tenthanhvien { get; set; }
        [Display(Name = "Mô tả sơ lược")]
        public string motasoluoc { get; set; }
        [Display(Name = "Chi tiết")]
        public string chitiet { get; set; }
        [Display(Name = "Đường dẫn hình ảnh")]
        public string urlhinhanh { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<int> nguoitao { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }
        [Display(Name = "Ghi chú")]
        public string ghichu { get; set; }
        [Display(Name = "Thứ tự hiện")]
        public Nullable<int> thutuhienthi { get; set; }
        public string tenthanhvien_en { get; set; }
        public string motasoluoc_en { get; set; }
        public string chitiet_en { get; set; }
        public virtual admin_account admin_account { get; set; }
        public virtual admin_nhomthanhvien admin_nhomthanhvien { get; set; }
    }
    #endregion


    // 
    // Nhóm thành viên
    //
    #region Nhóm thành viên
    [MetadataType(typeof(admin_nhomthanhvienMetaData))]
    public partial class admin_nhomthanhvien
    { }
    public partial class admin_nhomthanhvienMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên nhóm thành viên")]
        public string tennhomthanhvien { get; set; }
        [Display(Name = "Mô tả nhóm")]
        public string motanhom { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<int> nguoitao { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }
        [Display(Name = "Ghi chú")]
        public string ghichu { get; set; }
        [Display(Name = "Hình đại diện")]
        public string hinhdaidien { get; set; }

        public virtual admin_account admin_account { get; set; }
    }
    #endregion

    // 
    // Banner
    //
    #region Banner
    [MetadataType(typeof(bannerMetaData))]
    public partial class banner
    { }
    public partial class bannerMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên banner")]
        public string tenbanner { get; set; }
        [Display(Name = "Đường dẫn banner")]
        public string urlbanner { get; set; }
        [Display(Name = "Mô tả banner")]
        public string motabanner { get; set; }
        [Display(Name = "Ghi chú")]
        public string ghichu { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<int> nguoitao { get; set; }
        [Display(Name = "Hiển thị")]
        public Nullable<bool> hienthi { get; set; }
        [Display(Name = "Thứ tự hiện")]
        public Nullable<int> thutuhien { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }
        [Display(Name = "Loại banner")]
        public Nullable<int> loaibanner { get; set; }
        public virtual admin_account admin_account { get; set; }
        public virtual loaibanner loaibanner1 { get; set; }
    }
    #endregion

    // 
    // Loại Banner
    //
    #region Loại banner
    [MetadataType(typeof(loaibannerMetaData))]
    public partial class loaibanner
    { }
    public partial class loaibannerMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên loại banner")]
        public string tenloaibanner { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<banner> banners { get; set; }
    }
    #endregion

    // 
    // Liên hệ
    //
    #region Liên hệ
    [MetadataType(typeof(admin_baivietlienheMetaData))]
    public partial class admin_baivietlienhe
    { }
    public partial class admin_baivietlienheMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên bài liên hệ")]
        public string tenbailienhe { get; set; }
        [Display(Name = "Nội dung")]
        public string noidung { get; set; }
        [Display(Name = "Hiển thị")]
        public Nullable<bool> hienthi { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<int> nguoitao { get; set; }
        [Display(Name = "Ghi chú")]
        public string ghichu { get; set; }
        public string tenbailienhe_en { get; set; }
        public string noidung_en { get; set; }

        public virtual admin_account admin_account { get; set; }
    }
    #endregion

    // 
    // Giới thiệu
    //
    #region Giới thiệu
    [MetadataType(typeof(admin_gioithieuMetaData))]
    public partial class admin_gioithieu
    { }
    public partial class admin_gioithieuMetaData
    {
        public int id { get; set; }
        [Display(Name = "Tên bài giới thiệu")]
        public string tenbaigioithieu { get; set; }
        [Display(Name = "Nội dung")]
        public string noidung { get; set; }
        [Display(Name = "Hiển thị")]
        public Nullable<bool> hienthi { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public Nullable<int> thutuhienthi { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> daxoa { get; set; }
        [Display(Name = "Người tạo")]
        public int nguoitao { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        [Display(Name = "Ghi chú")]
        public string ghichu { get; set; }
        public string tenbaigioithieu_en { get; set; }
        public string noidung_en { get; set; }
        public virtual admin_account admin_account { get; set; }
    }
    #endregion

    // 
    // Thông tin cố định
    //
    #region Thông tin cố định
    [MetadataType(typeof(admin_thongtincodinhMetaData))]
    public partial class admin_thongtincodinh
    { }
    public partial class admin_thongtincodinhMetaData
    {
        public int id { get; set; }
        [Display(Name = "Địa chỉ")]
        public string diachi { get; set; }
        [Display(Name = "Điện thoại")]
        public string dienthoai { get; set; }
        [Display(Name = "Website")]
        public string website { get; set; }
        [Display(Name = "Fax")]
        public string fax { get; set; }
        [Display(Name = "Mail")]
        public string mail { get; set; }
        [Display(Name = "Logo")]
        public string logo { get; set; }
        [Display(Name = "Địa chỉ Gmap")]
        public string diachigmap { get; set; }
        [Display(Name = "Bài viết trang chủ")]
        public string baiviettrangchu { get; set; }
        public string diachi_en { get; set; }
        public string baiviettrangchu_en { get; set; }
    }
    #endregion

    // 
    // Loại sản phẩm
    //
    [MetadataType(typeof(loaisanphamMetadata))]
    public partial class loaisanpham
    { }
    public partial class loaisanphamMetadata
    {
        public int id { get; set; }
        [Display(Name = "Tên loại sản phẩm")]
        public string ten { get; set; }
        public DateTime ngaytao { get; set; }
        public DateTime ngaycapnhat { get; set; }
        public int nguoitao { get; set; }
        public bool daxoa { get; set; } = false;
        public string hinhanhloaisanpham { get; set; }
        public string motasoluoc { get; set; }
        public string ghichu { get; set; }
        public bool hienthi { get; set; } = true;
        public int thutuhien { get; set; } = 1;
    }


    // 
    // Sản phẩm
    //
    [MetadataType(typeof(sanphamMetadata))]
    public partial class sanpham
    { }
    public partial class sanphamMetadata
    {
        [Display(Name = "Mã sản phẩm")]
        [Key]
        public string id { get; set; }
        public string ten { get; set; }
        public int loaisanpham { get; set; }
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
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int giaban { get; set; }
        public Nullable<int> mathuonghieu { get; set; }
        public virtual loaisanpham loaisanpham1 { get; set; }
    }
}