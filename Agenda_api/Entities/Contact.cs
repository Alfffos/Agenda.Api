
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Esta clase contact la entidad que usa el profe(saque del notion).
//namespace Agenda_api.Entities
//{
//    public class Contact
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public long CelularNumber { get; set; }
//        public long? TelephoneNumber { get; set; }
//        public string Description { get; set; }

//        public User UserId { get; set; }
//    }
//}











// Estas lineas de codigo las habia heco en base al video turorial.


namespace Agenda_api.Entities;

// esta es la entidad contacto que vamos a usar (pero no vamos a mapear la base de datos con esto)
public class Contact
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public int? CelularNumber { get; set; }
    public int? TelephoneNumber { get; set; }
    public string Description = string.Empty;
    [ForeignKey("UserId")]
    public User User { get; set; }
    public int UserId { get; set; }
}



