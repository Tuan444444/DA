using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Models
{
    [Table("HopDong")]
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

        // KHÓA NGOẠI
        [ForeignKey("MaNguoiThue")]
        public NguoiThue NguoiThue { get; set; }

        [ForeignKey("MaPhong")]
        public Phong Phong { get; set; }

        // Quan hệ ngược
        public ICollection<HoaDon> HoaDons { get; set; }
    }

}
