using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace DA.Models
{
    public class Phong_DichVu
    {
        public int MaPhong { get; set; }
        public int MaDichVu { get; set; }

        public DateTime NgayApDung { get; set; }

        [ForeignKey(nameof(MaPhong))]
        public virtual Phong Phong { get; set; }

        [ForeignKey(nameof(MaDichVu))]
        public virtual DichVu DichVu { get; set; }
    }
}
