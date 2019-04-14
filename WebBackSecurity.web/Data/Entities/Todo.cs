using System.ComponentModel.DataAnnotations;

namespace WebBackSecurity.web.Data.Entities
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}