using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class NguoiThue
{
    [Key]
    public int MaNguoiThue { get; set; }

    [Required]
    public int MaTaiKhoan { get; set; }

    [ForeignKey("MaTaiKhoan")]
    public virtual TaiKhoan TaiKhoan { get; set; }

    [Required]
    [StringLength(100)]
    public string HoTen { get; set; }

    [Required]
    [StringLength(12)]
    public string CCCD { get; set; }

    [Required]
    [StringLength(15)]
    public string SoDienThoai { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
