using Agenda_api.Models.DTOs;
using Agenda_api.Entities;

namespace Agenda_api.Repository.Interfaces
{
    public interface IUserRepository
    {

        Task<List<User>> GetAll();
        Task<User> GetById(int id);

        Task Delete(User user);
        Task Archive (User user);
        Task <User> Create(User user);
        Task Update(CreateAndUpdateUser dto);

        //Task<User> Validate(string name);

        //Task<User> Create(CreateAndUpdateUser user);
        // Task




        //public User? Validate(AutenticationRequestBody authRequestBody);
        //public User? GetById(int userId);
        //public List<User> GetAll();
        //public void Create(CreateAndUpdateUser dto);
        //public void Update(CreateAndUpdateUser dto);
        //public void Delete(int id);
        //public void Archive(int id);
        //public void Update(User dto_maped);        // nuevo metodo generado por vscode
    }
}
