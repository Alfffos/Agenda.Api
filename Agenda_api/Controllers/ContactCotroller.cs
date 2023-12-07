 
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
        public async Task<IActionResult> GetAll()                   //Este metodo sirve para listar todos los usuarios 
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);     //Accedo a la claim
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

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateAndUpdateContact createContactDto)
        {
            try
            {
                
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);          // Con esto se accede a las claims del Dto
                await _contactRepository.Create(createContactDto, userId);

                
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
            try
            {
                await _contactRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


        [HttpGet]
        [Route("favs")]
        public async Task<IActionResult> GetFavorits()
        {
            try
            {
                int id = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                List<Contact> contacts_fav = await _contactRepository.Get_fav(id);
                if(contacts_fav.Count > 0)      //si no hay ningun contacto en fav que retorne NotFound.
                {
                    return Ok(contacts_fav);
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
    }
}
