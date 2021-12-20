using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(30)]
        [Required]
        public string? Email { get; set; }
        [Required]
        public int? Age { get; set; }
        public bool Active { get; set; }
    }
}
