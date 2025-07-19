using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class DichVu
    {
        [Key]
        public int MaDichVu { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenDichVu { get; set; }

        public decimal DonGia { get; set; }

        [MaxLength(50)]
        public string DonViTinh { get; set; }

        // Quan hệ ngược với ChiTietHoaDon
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        // Quan hệ ngược với Phong_DichVu
        public ICollection<Phong_DichVu> Phong_DichVus { get; set; }
    }

}
