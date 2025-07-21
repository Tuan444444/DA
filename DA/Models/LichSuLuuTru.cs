using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Models
{
    [Table("LichSuLuuTru")]
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
