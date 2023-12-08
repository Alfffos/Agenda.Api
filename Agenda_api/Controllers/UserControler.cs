using Agenda_api.Models;
using Agenda_api.Repository;
using Agenda_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Agenda_api.Models.DTOs;
using Agenda_api.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;
using Agenda_api.Data;

namespace Agenda_api.Controllers


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
        public async Task<IActionResult> GetOneById(int Id)
        {
            try
            {
                var User_Id = await _userRepository.GetById(Id);

                if (User_Id == null)
                {
                    return NotFound();
                }
                var dto = _automapper.Map<GetUserByIdResponse>(User_Id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            //Consumo el dto GetUserById ya mappeado de User.



        }
        
        [HttpPost]
        [AllowAnonymous]    // Con esto hago que no se requiera el token de authentication para crear un nuevo usuario.
        public async Task<IActionResult> CreateUser(CreateAndUpdateUser dto)             //Metodo para crear un User
        {
            try
            {

                await _userRepository.Create(dto);                                 //Uso el metodo Create de la interface IUserRepository.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("Created", dto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id_user, CreateAndUpdateUser dto)       //Actualizo User.
        {
            try
            {


                var userItem = await _userRepository.GetById(id_user); //Traigo el User de la DB para saber si existe.

                if (userItem == null)
                {
                    return NotFound();  //retorno NotFound si es que no lo encuentra.
                }

                await _userRepository.Update(id_user, dto);    //Paso como parámetro el dto CreateAndUpdateUser que contiene los nuevos datos y el id para buscar el usuario en el repo.

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            try
            {
                var user = await _userRepository.GetById(Id);                //busco el user 

                if (user== null)                                           // pregunto si es nulo, de serlo retorno Not Found.
                {
                    return NotFound();
                }

                if (user.Rol== 0)
                {
                    await _userRepository.Archive(Id);                    // si es un user con ROL admin lo archivo y si no lo remuevo.


                }
                else
                {
                    await _userRepository.Delete(Id);
                }

                return NoContent();


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
