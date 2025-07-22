using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Models
{
public class NguoiThue
{
    [Key]
    public int MaNguoiThue { get; set; }


    public int MaTaiKhoan { get; set; } // FK

    public string HoTen { get; set; }
    public string CCCD { get; set; }
    public string SoDienThoai { get; set; }
    public string Email { get; set; }
    public string DiaChi { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(MaTaiKhoan))]
        public virtual TaiKhoan TaiKhoan { get; set; } // Điều hướng
}
}

