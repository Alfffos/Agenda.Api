using Agenda_api.Models.DTOs;
using Agenda_api.Entities;

                                                //Las interfaces sirven para indicar el funcionamiento de cada metodo del repository
namespace Agenda_api.Repository.Interfaces
{
    public interface IUserRepository
    {

        Task<List<User>> GetAll();
        Task<User> GetById(int id);

        Task Delete(int id);
        Task Archive (int id);
        Task Create(CreateAndUpdateUser user);
        Task Update(int id_user, CreateAndUpdateUser dto);
        Task<User> Validate(AutenticationRequestBody authRequestBody);

    }
}
