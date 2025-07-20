using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Models
{
    [Table("NguoiThue")]
    public class NguoiThue
    {
        [Key]
        public int MaNguoiThue { get; set; }

        [Required]
        public int MaTaiKhoan { get; set; }

        [Required]
        public string HoTen { get; set; }

        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }

        [BindNever] // ⚠️ Phải có dòng này, KHÔNG dùng [Required]
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
