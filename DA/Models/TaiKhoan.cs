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
        public string VaiTro { get; set; } // "chunha" hoặc "nguoithue"

        // Liên kết đến Chủ nhà (nếu là chủ nhà)
        public int? MaChuNha { get; set; }

        [ForeignKey("MaChuNha")]
        public ChuNha? ChuNha { get; set; }

        // Liên kết đến Người thuê (nếu là người thuê)
        public int? MaNguoiThue { get; set; }

        [ForeignKey("MaNguoiThue")]
        public NguoiThue? NguoiThue { get; set; }
    }
}
