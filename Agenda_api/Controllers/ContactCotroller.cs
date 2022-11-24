
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Agenda_api.Repository.Interfaces;
using Agenda_api.Models.DTOs;

namespace Agenda_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository=contactRepository;
        }

        [HttpGet]
        public IActionResult GetAll()                   //Creo un metodo GetAll() que consume de la interfaz de IContactRepositoy el metodo GetAll().
        {
            return Ok(_contactRepository.GetAll());
        }
        [HttpGet]
        [Route("{Id}")]

        public IActionResult GetOne(int id)
        {
            return Ok(_contactRepository.GetAll().Where(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult CreateContact(CreateAndUpdateContact createContactDto)
        {
            try
            {
                _contactRepository.Create(createContactDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("Created", createContactDto);
        }

        [HttpPut]
        public IActionResult UpdateContact(CreateAndUpdateContact dto)
        {
            try
            {
                _contactRepository.Update(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteContactById(int id)
        {
            try
            {
                _contactRepository.Delete(id);
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
