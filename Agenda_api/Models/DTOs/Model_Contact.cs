using Agenda_api.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda_api.Models.DTOs
{
    // aca estoy creando una clase Create_And_Update_Contatc_DTO para que la base de datos cree una tabla con estos datos
    public class Create_And_Update_Contatc_DTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CelularNumber { get; set; }
        public int? TelephoneNumber { get; set; }
        public string Description = String.Empty;
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }

    }
}
