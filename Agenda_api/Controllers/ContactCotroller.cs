 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Agenda_api.Repository.Interfaces;
using Agenda_api.Models.DTOs;
using SQLitePCL;

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
        public IActionResult GetAll()                   //Creo un metodo GetAll() que consume de la interfaz de IContactRepositoy el metodo GetAll().
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
            return Ok(_contactRepository.GetAllByUser(userId));
        }
        [HttpGet]
        [Route("{id}")]          // Saco los {} del "Get_Id" porque si pongo [] corchetes me agrega como propiedad nueva en la peticion del ID

        public IActionResult GetOne(int id)
        {
            try                                     
            {
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);       //Accedo a la Claim
                return Ok(_contactRepository.GetAllByUser(userId).Where(x => x.Id == id));
            }
            catch (Exception ex)                          // Agrego el try and catch para que tire un mensaje de error por las dudas.
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateContact(CreateAndUpdateContact createContactDto)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);          // Con esto se accede a las claims del Dto
            _contactRepository.Create(createContactDto, userId);
            return Created("Created", createContactDto);
        }

        [HttpPut]
        public IActionResult UpdateContact(CreateAndUpdateContact dto)
        {
            _contactRepository.Update(dto);
            return NoContent();

        }
        [HttpDelete]
        public IActionResult DeleteContactById(int id)
        {
            var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("role"));
            if(role.Value == "Admin")
            {
                
                _contactRepository.Delete(id);
            }
            else
            {
                _userRepository.Archive(id);
                
            }
            return NoContent();
        }
    }
}
