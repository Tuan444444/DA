using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
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
        public HopDong HopDong { get; set; }

        // Quan hệ ngược
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }

}
