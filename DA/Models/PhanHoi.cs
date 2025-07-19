using System.ComponentModel.DataAnnotations;

namespace DA.Models
{
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
        public NguoiThue NguoiThue { get; set; }
    }

}
