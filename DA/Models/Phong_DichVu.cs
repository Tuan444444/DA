using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
    public class Phong_DichVu
    {
        [Key, Column(Order = 0)]
        public int MaPhong { get; set; }

        [Key, Column(Order = 1)]
        public int MaDichVu { get; set; }

        public DateTime NgayApDung { get; set; }

        public Phong Phong { get; set; }
        public DichVu DichVu { get; set; }
    }


}
