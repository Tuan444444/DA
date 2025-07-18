using System.ComponentModel.DataAnnotations;

namespace DA.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        public string VaiTro { get; set; } // ChuNha | NguoiThue

        [Required]
        public string TenDangNhap { get; set; }

        [Required]
        public string MatKhau { get; set; }

        public int? MaChuNha { get; set; }
        public int? MaNguoiThue { get; set; }
    }

}
