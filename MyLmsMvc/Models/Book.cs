using System.ComponentModel.DataAnnotations;

namespace MyLmsMvc.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        public string Genre { get; set; }

        [Required]
        public int CopiesAvailable { get; set; }
    }

}
