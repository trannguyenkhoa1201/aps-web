using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiKiemTra03_02.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        // Foreign key to Author
        [Required]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        [ValidateNever]
        public Author Author { get; set; }
    }


}
