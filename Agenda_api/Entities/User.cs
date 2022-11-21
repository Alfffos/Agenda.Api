using Agenda_api.Entities;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Esta clase es una entidad que usa el profe (saque del Notion)
//namespace Agenda_api.Entities
//{
//    public class User
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public string Email { get; set; }                                         //Si uso esta entiti no me funciona la relacion de UserId con el Contac.
//        public string? LastName { get; set; }
//        public string Password { get; set; }
//        public string UserName { get; set; }
//        public ICollection<Contact>? Contacts { get; set; }
//    }
//}






// Esta entidad la cree en base el video tutorial.

namespace Agenda_api.Entities;

public class User
{
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
