using DA.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class NguoiThue
    {
        [Key]
        public int MaNguoiThue { get; set; }

        [Required]
        public string HoTen { get; set; }

        [Required]
        public string SDT { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CCCD { get; set; }

        // Navigation property nếu cần
        public TaiKhoan TaiKhoan { get; set; }
    }
}
