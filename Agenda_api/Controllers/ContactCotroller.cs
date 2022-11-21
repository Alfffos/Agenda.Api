using Agenda_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Agenda_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private ContactRepository _contactRepository { get; set; }  

        public ContactController(ContactRepository contactRepository)       //Inyecto el ContactRepository en el ContactController.
        {
            _contactRepository = contactRepository; 
        }

        [HttpGet] 
        public IActionResult GetAll()                   //Creo un metodo GetAll() para que me traiga todos los Contact.
        {
            return Ok(_contactRepository.GetAll());
        }
    }
}
