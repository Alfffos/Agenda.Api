using Agenda_api.Entities;
using Agenda_api.Models.DTOs;


namespace Agenda_api.Repository.Interfaces
{
    public interface IContactRepository
    {
        Task <List<Contact>> GetAllByUser(int id);
        Task Create(CreateAndUpdateContact dto, int userId);

        Task Update(int id,CreateAndUpdateContact dto);
        Task Delete(int id);




        //public List<Contact> GetAllByUser(int id);
        //public void Create(CreateAndUpdateContact dto, int id);
        //public void Update(CreateAndUpdateContact dto);
        //public void Delete(int id);
    }
}
