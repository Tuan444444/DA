using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class LichSuLuuTru
    {
        [Key]
        public int MaLichSu { get; set; }

        public int MaNguoiThue { get; set; }
        public int MaPhong { get; set; }

        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        // FK
        public NguoiThue NguoiThue { get; set; }
        public Phong Phong { get; set; }
    }

}
