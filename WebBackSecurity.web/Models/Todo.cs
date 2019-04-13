using System.ComponentModel.DataAnnotations;

namespace WebBackSecurity.web.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}