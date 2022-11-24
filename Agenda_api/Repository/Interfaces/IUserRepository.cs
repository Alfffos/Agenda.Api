using Agenda_api.Models.DTOs;
using Agenda_api.Entities;

namespace Agenda_api.Repository.Interfaces
{
    public interface IUserRepository
    {
        public User? Validate(AutenticationRequestBody aurhRequestBody);
        public User? GetById(int userId);
        public List<User> GetAll();
        public void Create(CreateAndUpdateUser dto);
        public void Update(CreateAndUpdateUser dto);
        public void Delete(int id);

    }
}
