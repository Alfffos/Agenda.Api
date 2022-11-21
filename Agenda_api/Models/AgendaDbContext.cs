
using Agenda_api.Entities;
using Agenda_api.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Agenda_api.Models

{ // Esta clase nos permite crear la base de datos, acceder a la base de datos, insertar nuevos registros, poder actualizarlos
    public class AgendaDbContext : DbContext   //agendadbcontext va a heredar de una clase que esta en .NET que se llama "DbContex" 
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
        {

        }
        // Aca estoy llamando a los Modelos User(Model_User) y Contact(Model_Contact) para que se mapeen en la base de datos 
        public DbSet<Create_And_Update_User_DTO> Users { get; set; }   
        public DbSet<Create_And_Update_Contatc_DTO> Contatc { get; set; }
    }     
}
