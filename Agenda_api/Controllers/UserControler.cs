using Agenda_api.Models;
using Agenda_api.Repository;
using Agenda_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Agenda_api.Models.DTOs;
using Agenda_api.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Agenda_api.Controllers

                  // Los controladores se encargan de manejar todas las peticiones del Front , en este caso consumiento la interfaz IUserRepository.
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMapper _automapper;
        private readonly IUserRepository _userRepository;          //Aca estoy inyectando el IUserRepository.
        public UserController(IUserRepository userRepository, IMapper automapper)            //Creo el constructor de la clase que recibe como paremetro un objeto  de tipo IUserRepository y lo llamo userRepository
        {
            _userRepository = userRepository;
            _automapper = automapper;
        }

        [HttpGet]
        public IActionResult GetAll()                   //Creo un metodo de HttpGet que me devuelve todos los usuarios.
        {
            return Ok(_userRepository.GetAll());       //Gracias a la inyeccion llamo al metodo "GetAllUsers()" del IUserRepository.
        }

        [HttpGet]
        [Route("{Id}")]                                              //Creo un metodo de HttpGet para que me devuelva el User por Id.
        public IActionResult GetOneById(int Id)
        {
            User user = _userRepository.GetById(Id);
            var dto = _automapper.Map<GetUserByIdResponse>(User);      //Consumo el GetUserByIdResponse desde el automapper.
            try
            {
               return Ok(dto);        
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUser(CreateAndUpdateUser dto)             //Metodo para crear un User
        {
            try
            {
                _userRepository.Create(dto);                                 //Uso el metodo Create de la interface IUserRepository.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("Created", dto);
        }

        [HttpPut]
        public IActionResult UpdateUser(CreateAndUpdateUser dto)       //Actualizo User.
        {
            try
            {
                _userRepository.Update(dto);                //Paso como parámetro el dto CreateAndUpdateUser que contiene los nuevos datos.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpDelete]                                                                                       
        public IActionResult DeleteUser(int Id)                                 //User Delet                
        {
            try
            {   if(_userRepository.GetById(Id).Name =="Admin")
                {
                    _userRepository.Delete(Id);         //Paso como parametro el Id.
                }
                else
                {
                    _userRepository.Archive(Id);
                }
                return StatusCode(204);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }     
    }
}
