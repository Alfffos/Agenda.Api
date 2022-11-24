using Agenda_api.Entities;
using Agenda_api.Models;
using Agenda_api.Models.DTOs;
using AutoMapper;

// Esta clase es la que usa el automapper, lo que hace es mapear las propiedades del los objetos.
namespace Agenda_api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, CreateAndUpdateUser>();
        }

    }
}
