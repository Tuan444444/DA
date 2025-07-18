using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Phong
{
    [Key]
    public int MaPhong { get; set; }

    [ForeignKey("ChuNha")]
    public int MaChuNha { get; set; }

    [Required]
    public string TenPhong { get; set; }

    public string LoaiPhong { get; set; }

    [Required]
    public decimal GiaPhong { get; set; }

    [Required]
    public double DienTich { get; set; }

    public string TrangThai { get; set; }

    public virtual ChuNha ChuNha { get; set; }
}
