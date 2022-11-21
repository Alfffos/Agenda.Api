using System.ComponentModel.DataAnnotations;

namespace Agenda_api.DTOs
{
    public class UserForCreationDTO   //Este DTO es para la creacion de un nuevo Usuario.
    {
        public int Id { get; set; }
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; }
    }
}