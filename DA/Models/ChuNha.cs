using DA.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class ChuNha
    {
        [Key]
        public int MaChuNha { get; set; }

        [Required]
        public string HoTen { get; set; }

        [Required]
        public string SDT { get; set; }

        [Required]
        public string Email { get; set; }

        public string DiaChi { get; set; }

        // Navigation property nếu cần
        public TaiKhoan TaiKhoan { get; set; }
    }
}
