using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class TaiKhoan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TenDangNhap { get; set; }

        [Required]
        public string MatKhau { get; set; }

        [Required]
        public string VaiTro { get; set; } // "ChuNha" hoặc "NguoiThue"

        public int? MaChuNha { get; set; } // nullable

        public int? MaNguoiThue { get; set; } // nullable
    }

}
