using Agenda_api.Models.DTOs;
using Agenda_api.Entities;

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

        //Task<User> Create(CreateAndUpdateUser user);
        //public User? Validate(AutenticationRequestBody authRequestBody);
        //public User? GetById(int userId);
        //public List<User> GetAll();
        //public void Create(CreateAndUpdateUser dto);
        //public void Update(CreateAndUpdateUser dto);
        //public void Delete(int id);
        //public void Archive(int id);
    }
}
