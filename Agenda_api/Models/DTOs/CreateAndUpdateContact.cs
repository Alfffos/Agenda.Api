using Agenda_api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Agenda_api.Models.DTOs
{
    public class CreateAndUpdateContact
    {
        [Required]
        public string Name { get; set; }
        public int? CelularNumber { get; set; }
        public int? TelephoneNumber { get; set; }
        public bool favorite { get; set; }

        public string Description = String.Empty;
        public User? User;

        // falta 
        
    }
}
