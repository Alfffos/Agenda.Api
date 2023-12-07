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

        Task <List<Contact>> Get_fav(int id);
    }
}
