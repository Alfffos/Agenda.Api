using Agenda_api.Entities;
using Agenda_api.Models.DTOs;


namespace Agenda_api.Repository.Interfaces
{
    public interface IContactRepository
    {
        public List<Contact> GetAll();
        public void Create(CreateAndUpdateContact dto);
        public void Update(CreateAndUpdateContact dto);
        public void Delete(int id);
    }
}
