using Agenda_api.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda_api.Models.DTOs
{
    public class Create_And_Update_User_DTO
    {
        // El User_id se lo da la base de datos relacional para idenfiticarlo y tiene que ser auto incremental. Por eso mismo en este DTO no aparece
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public ICollection<Contact> Contacts { get; set; }

    }
}
