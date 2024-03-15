using System.ComponentModel.DataAnnotations;

namespace MyLmsMvc.Models
{
    public class Reader
    {
        [Key] public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string ContactNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        // Optional: Additional properties such as Address, MembershipDate, etc.

    }

}
