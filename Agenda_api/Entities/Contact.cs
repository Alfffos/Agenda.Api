﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda_api.Entities;

// esta es la entidad contacto que vamos a usar (pero no vamos a mapear la base de datos con esto)
public class Contact
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? CelularNumber { get; set; }
    public string? TelephoneNumber { get; set; }
    public string Description = string.Empty;
    [ForeignKey("UserId")]
    public User User { get; set; }
    public int UserId { get; set; }
    public bool Favorite { get; set; }  

}



