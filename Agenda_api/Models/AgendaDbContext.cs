
using Agenda_api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Agenda_api.Models

{
    public class AgendaDbContext : DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }     
}
