using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }

        public int MaHopDong { get; set; }

        public DateTime NgayLap { get; set; }

        public decimal TongTien { get; set; }

        [MaxLength(50)]
        public string TrangThaiThanhToan { get; set; }

        // FK
        [ForeignKey("MaHopDong")]
        public HopDong HopDong { get; set; }

        // Quan hệ ngược
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
