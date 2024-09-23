using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models
{
    public class SanPham
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public double price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        [ForeignKey("TheloaiId")]
        public TheLoai Theloai { get; set; }
    }
}
