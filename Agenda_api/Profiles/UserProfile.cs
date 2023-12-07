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
            //Estos 2 maps sirven para enviar los Dto sin los datos sesibles.
            CreateMap<User, CreateAndUpdateUser>();                              // Primero le paso la entidad a mapear en este caso User, y luego la propiedad
            CreateMap<User, GetUserByIdResponse>();                              // a la cual quiero que mapee, en este caso es  un Dto.

            //Estos 2 mapas sirver para recibir datos de creacion de parte del front
            CreateMap<CreateAndUpdateUser, User>();
            //CreateMap<GetUserByIdResponse, User>();           //Este no se usa, ya que "GetUserById" es para devolver datos y no me sirve de nada mapear ese dto a User.
            

        }

    }
}
