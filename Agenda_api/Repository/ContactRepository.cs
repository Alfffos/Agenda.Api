using Agenda_api.Data;
using Agenda_api.Entities;
using Agenda_api.Models.DTOs;
using Agenda_api.Repository.Interfaces;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace Agenda_api.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AgendaApiContext _context;
        private readonly IMapper _mapper;
        public ContactRepository(AgendaApiContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public void Create(CreateAndUpdateContact dto, int id)
        {
            Contact contact = _mapper.Map<Contact>(dto);
            contact.UserId = id;
            _context.Contacts.Add(contact);
            _context.SaveChanges();
           
        }

        public void Delete(int id)
        {
            _context.Contacts.Remove(_context.Contacts.Single(c => c.Id == id));      
            _context.SaveChanges();
            
        }

        public List<Contact> GetAllByUser(int id)
        {
            return _context.Contacts.Where(c => c.User.Id == id).ToList();
        }

        public void Update(CreateAndUpdateContact dto)
        {
            
            _context.Contacts.Update(_mapper.Map<Contact>(dto));
            _context.SaveChanges();

        }
        //public void Update(CreateAndUpdateContact dto)
        //{
        //    Contact New_contact = _mapper.Map<Contact>(dto);

        //    _context.Contacts.Update(_mapper.Map<Contact>(New_contact));
        //    _context.SaveChanges();
        //}
    }

}
