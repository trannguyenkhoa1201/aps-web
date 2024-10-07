using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace BaiKiemTra03_02.Models

{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public int BirthYear { get; set; }
    }
}
