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
                return BadRequest(ex .Message);
            }
            
            
                       //Consumo el dto GetUserById ya mappeado de User.
            
            
            
            //try
            //{
            //   return Ok(dto);        
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateAndUpdateUser dto)             //Metodo para crear un User
        {
            try
            {
                var user = _automapper.Map<User>(dto);

                //new_user = await _userRepository.CreateUser(new_user);

                await _userRepository.Create(user);                                 //Uso el metodo Create de la interface IUserRepository.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
            return Created("Created", dto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id_user,CreateAndUpdateUser dto)       //Actualizo User.
        {
            try
            {
                  
                  
                var userItem = await _userRepository.GetById(id_user); //Traigo el User de la DB para saber si existe.

                if (userItem == null)
                {
                    return NotFound();  //retorno NotFound si es que no lo encuentra.
                }

                await _userRepository.Update(id_user,dto);    //Paso como parámetro el dto CreateAndUpdateUser que contiene los nuevos datos y el id para buscar el usuario en el repo.
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(int Id,CreateAndUpdateUser dto)
        //{
        //     await _context.Users.FindAsync(Id);
        //   // User user =  _userRepository.GetById(Id);

        //    User dto_maped =  _automapper.Map<User>(dto);

        //    _userRepository.Update(dto_maped);            //Vs Code me genero un metodo en IUserRepository
        //    await _context.SaveChangesAsync();

        //    //var New_Put_User = await _automapper.Map<CreateAndUpdateUser>(Put_User);

        //    return NoContent();


        //    //try
        //    //{
        //    //    if (id != _userRepository.user.Id)
        //    //    {
        //    //        return BadRequest();
        //    //    }
        //    //}
        //    //catch(Exception ex)
        //    //{
        //    //    return BadRequest(ex.Message);
        //    //}
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            try
            {
                var user = await _userRepository.GetById(Id);                //busco el user 

                if (user == null)                                           // pregunto si es nulo, de serlo retorno Not Found.
                {
                    return NotFound();
                }

                if (user.Rol== 0)
                {
                    await _userRepository.Archive(user);                    // si es un user con ROL admin lo archivo y si no lo remuevo.

                     
                }
                else
                {
                   await _userRepository.Delete(user);
                }

                return NoContent();
                

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






        //[HttpDelete]
        //public async Task<IActionResult> DeleteUser(int Id)                                 //User Delet                
        //{
        //    try
        //    {
        //        if (_userRepository.GetById(Id).Role ==0)        //En esta parte seguramente hay que cambiar esta condicion y preguntar si el ROL es 1 o 0
        //        {
        //            _userRepository.Archive(Id);         //Paso como parametro el Id.
        //        }
        //        else
        //        {
        //            _userRepository.Delete(Id);
        //        }
        //        return StatusCode(204);


        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        return BadRequest(ex.Message);
        //        //    } 
        //        //}     
        //    }
    }
