using Agenda_api.Models;
using Agenda_api.Repository;
using Agenda_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Agenda_api.DTOs;

namespace Agenda_api.Controllers

                  // Los controladores se encargan de manejar todas las peticiones del Front End hacia el Back End y desde el Back End hacia el Front End.
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository { get; set; }            //Aca estoy inyectando el repository
        public UserController(UserRepository userRepository)            //Creo el constructor de la clase que recibe como paremetro un objeto  de tipo UserRepository y lo llamo userRepository
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAll()                   //Creo un metodo de HttpGet que me devuelve todos los usuarios.
        {
            return Ok(_userRepository.GetAllUsers());       //Gracias a la inyeccion llamo al metodo "GetAllUsers()" del repository.
        }

        [HttpGet]
        [Route("GetOne/{Id}")]                                              //Creo un metodo de HttpGet para que me devuelva el User por Id.
        public IActionResult GetOneById(int Id)
        {
            
            //List<User> usersToReturn = _userRepository.GetAllUsers();
            //usersToReturn.Where(x => x.Id == Id).ToList();                   //Este codigo estaba en el Notion, pero no retorna x id, hace un getAll.
            //if (usersToReturn.Count > 0)
            //    return Ok(usersToReturn);
            //return NotFound("Usuario inexistente"); 
            
            List<User> UserForId = _userRepository.GetAllUsers().Where(x => x.Id == Id).ToList();
            if (UserForId.Count > 0)                                                                        //Esta sentecia LINQ la programe para que busque x id.
                return  Ok(UserForId);
            return NotFound("Usuario inexistente"); //Si no encuentra ningun user retorna un NotFound
        }

        [HttpPost]
        public IActionResult CreateUser(UserForCreationDTO UserDTO)             //Metodo para crear un User
        {
            _userRepository.CreateUser(UserDTO);                     //Gracias a la iyeccion uso el metodo CreateUser del Repository

            return NoContent();                       // ACA NO SE PORQUE RETORNA UN NOCONTENT EN VEZ DE UN OK.    

        }

        
            
    }
}
