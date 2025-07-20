using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Models
{
   
    public class ChiTietHoaDon
    {
        [Key]
        public int MaChiTiet { get; set; }

        public int MaHoaDon { get; set; }
        [ForeignKey("MaHoaDon")]
        public HoaDon HoaDon { get; set; }

        public int MaDichVu { get; set; }
        [ForeignKey("MaDichVu")]
        public DichVu DichVu { get; set; }

        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
