using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBackSecurity.web.ViewModels.Todo
{
    public class TodoViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [DisplayName("Navn")]
        public string Name { get; set; }

        [MaxLength(255)]
        [DisplayName("Beskrivelse")]
        public string Description { get; set; }

        [DisplayName("Oprettet")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedDateTime { get; set; }

        [DisplayName("Udført")] public bool IsDone { get; set; }
    }
}