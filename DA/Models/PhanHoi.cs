using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Models
{
    [Table("PhanHoi")]
    public class PhanHoi
    {
        [Key]
        public int MaPhanHoi { get; set; }

        public int MaNguoiThue { get; set; }

        [Required]
        public string NoiDung { get; set; }

        public DateTime NgayGui { get; set; }

        public string KetQuaXuLy { get; set; }

        public DateTime? NgayXuLy { get; set; }

        // FK
        [ForeignKey("MaNguoiThue")]
        public NguoiThue NguoiThue { get; set; }
    }

}
