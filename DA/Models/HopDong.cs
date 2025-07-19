using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class HopDong
    {
        [Key]
        public int MaHopDong { get; set; }

        public int MaNguoiThue { get; set; }
        public int MaPhong { get; set; }

        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public decimal TienDatCoc { get; set; }

        [MaxLength(50)]
        public string TrangThai { get; set; }

        // FK
        public NguoiThue NguoiThue { get; set; }
        public Phong Phong { get; set; }

        // Quan hệ ngược
        public ICollection<HoaDon> HoaDons { get; set; }
    }

}
