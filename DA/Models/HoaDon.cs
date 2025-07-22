using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Models
{
    public class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }

        public int MaHopDong { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(MaHopDong))]
        public  HopDong HopDong { get; set; }

        public DateTime NgayLap { get; set; }

        public decimal TongTien { get; set; }

        [MaxLength(50)]
        public string TrangThaiThanhToan { get; set; }

        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    }
}