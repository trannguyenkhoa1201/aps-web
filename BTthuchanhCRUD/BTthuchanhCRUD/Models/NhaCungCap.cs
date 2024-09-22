using System.ComponentModel.DataAnnotations;

namespace BTthuchanhCRUD.Models
{
    public class NhaCungCap
    {
        [Key]
        public int Id { get; set; }      // Khóa chính
        [Required]
        public string Ten { get; set; }   // Tên nhà cung cấp
        public string DiaChi { get; set; } // Địa chỉ
        public string SDT { get; set; }    // Số điện thoại
    }

}
