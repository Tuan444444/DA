using System.ComponentModel.DataAnnotations;

namespace DA.ViewModels  // <---- KHỚP VỚI FOLDER !
{
    public class RegisterViewModel
    {
        [Required]
        public string VaiTro { get; set; } // "ChuNha" hoặc "NguoiThue"

        [Required]
        public string HoTen { get; set; }

        [Required]
        public string SDT { get; set; }

        [Required]
        public string Email { get; set; }

        public string DiaChi { get; set; } // Chỉ dùng nếu là Chủ nhà

        public string CCCD { get; set; } // Chỉ dùng nếu là Người thuê

        [Required]
        public string TenDangNhap { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Required]
        [Compare("MatKhau")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
    }

}
