﻿using Agenda_api.Entities;
using Agenda_api.Models.Enum;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
    public State State { get; set; } = State.Active;

    public Rol Rol { get; set; } = Rol.User;
}
