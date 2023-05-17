 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Agenda_api.Repository.Interfaces;
using Agenda_api.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Agenda_api.Entities;

namespace Agenda_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUserRepository _userRepository;
        public ContactController(IContactRepository contactRepository, IUserRepository userRepository)
        {
            _contactRepository=contactRepository;
            _userRepository=userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()                   //Creo un metodo GetAll() que consume de la interfaz de IContactRepositoy el metodo GetAll().
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
            return Ok(await _contactRepository.GetAllByUser(userId));
        }
        [HttpGet]
        [Route("{id}")]          // Saco los {} del "Get_Id" porque si pongo [] corchetes me agrega como propiedad nueva en la peticion del ID

        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);   //Accedo a la Claim
                List<Contact> contacts = await _contactRepository.GetAllByUser(userId);  //Paso los contactos a una Lista de contactos para poder buscarlos.
                Contact contact = contacts.FirstOrDefault(x => x.Id == id);             //Lo busco en la lista.
                if (contact != null)
                {
                    return Ok(contact);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public IActionResult GetOne(int id)
        //{
        //    try                                     
        //    {
        //        int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);       //Accedo a la Claim
        //        return Ok(_contactRepository.GetAllByUser(userId).Where(x => x.Id == id));
        //    }
        //    catch (Exception ex)                          // Agrego el try and catch para que tire un mensaje de error por las dudas.
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    return NoContent();
        //}

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateAndUpdateContact createContactDto)
        {
            try
            {
                
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);          // Con esto se accede a las claims del Dto
                await _contactRepository.Create(createContactDto, userId);

                //return NoContent();
                return Created("Created", createContactDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task <IActionResult> UpdateContact(int id,CreateAndUpdateContact dto)
        {
            try
            {
                await _contactRepository.Update(id, dto);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteContactById(int id)
        {
            var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("role"));
            if(role.Value == "Admin")
            {
                
                await _contactRepository.Delete(id);
            }
            else
            {
                await _userRepository.Archive(id);
                
            }
            return NoContent();
        }
    }
}
