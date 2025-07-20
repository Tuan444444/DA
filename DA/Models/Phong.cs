using DA.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DA.Models // 🔧 Thêm dòng này
{
    [Table("Phong")]
    public class Phong
    {
        [Key]
        public int MaPhong { get; set; }

      //  [ForeignKey("MaChuNha")]
        public int MaChuNha { get; set; }

        [Required]
        public string TenPhong { get; set; }

        public string LoaiPhong { get; set; }

        [Required]
        public decimal GiaPhong { get; set; }

        [Required]
        public double DienTich { get; set; }

        public string TrangThai { get; set; }
        [ForeignKey(nameof(MaChuNha))]
        public virtual ChuNha ChuNha { get; set; }
        public ICollection<Phong_DichVu> Phong_DichVus { get; set; }
    }
}