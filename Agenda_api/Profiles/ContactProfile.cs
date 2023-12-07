using Agenda_api.Entities;
using Agenda_api.Models.DTOs;
using AutoMapper;

// Esta clase se usa para mapear las propiedades de la clase Contact.
namespace Agenda_api.Profiles
  
{
    public class ContactProfile : Profile
    {
        public ContactProfile() 
        {
            CreateMap<CreateAndUpdateContact, Contact>();
        }
    }
}
