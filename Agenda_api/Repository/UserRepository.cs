
using Agenda_api.DTOs;
using Agenda_api.Entities;

namespace Agenda_api.Repository
{
    public class UserRepository
    {
        List<User> FakeUsers = new List<User>()
            {
                new User()      //En esta lista estoy creando 2 objets de tipo usuarios fake para probar (SOLO PARA PROBAR!!)
                {
                    Email = "a@asdasd.com", 
                    Name = "Pablo",
                    Password = "passwordresegura",
                    Id = 1,
                },
                new User()                                // !!! Esto lo tengo que cambiar para hacer que el repository trabajo con la base de datos porque ahora esta trabajando con estos 2 FakeUsers.
                {
                    Email = "b@asdasd.com",
                    Name = "Mateo",
                    Password = "passwordresegura1",
                    Id = 2,
                }
            };
        public List<User> GetAllUsers()
        {
            return FakeUsers; //en este metodo le estoy retornando una lista de Fake users, osea los 2 usuarios que estan arriba.
        }
        public bool CreateUser(UserForCreationDTO UserDTO) //Este metodo CreateUser de tipo bool crea un usuario con los parametros del DTO y retorna true
        {
            User user = new User()
            {
                Name = UserDTO.Name,
                Password = UserDTO.Password,
                Id = UserDTO.Id,
                Email = UserDTO.Email,
            };
            FakeUsers.Add(user);
            return true;
        }
    }
}
