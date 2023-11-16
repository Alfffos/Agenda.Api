using Agenda_api.Data;
using Agenda_api.Entities;
using Agenda_api.Models.DTOs;
using Agenda_api.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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


        public async Task Create(CreateAndUpdateContact dto, int userId)   // TENGO QUE ASGNARLE EL VALOR DEL ID EN EL DTO PORQUE ES NULO.
        {

            var new_contact = new Contact() // Aca el profe mapeo a mano el DTO de CreatAndUpdateContact a Contac
            {
                CelularNumber = dto.CelularNumber,
                Description = dto.Description,
                Name = dto.Name,
                TelephoneNumber = dto.TelephoneNumber,
                UserId = userId                                // aca es donde se le asigna el usuario al contacto creado.
   
            };


            await _context.Contacts.AddAsync(new_contact);
            await _context.SaveChangesAsync();
            //Contact new_contact = _mapper.Map<Contact>(dto);
            // _context.Contacts.AddAsync(new_contact);
            // await _context.SaveChangesAsync();


        }

        public async Task Delete(int id)
        {
           
            _context.Contacts.Remove(await _context.Contacts.SingleAsync(c => c.Id == id));
           await _context.SaveChangesAsync();
        }

        public async Task<List<Contact>> GetAllByUser(int id)
        {
            return await _context.Contacts.Where(c => c.User.Id == id).ToListAsync();
        }

        public async Task<List<Contact>> Get_fav(int id)  //Recibe el id del User como parametro y devuelve una lista de tipo Contacts.
        {
            return await _context.Contacts.Where(u => u.User.Id == id && u.Favorite).ToListAsync(); // Consulta por el Usuario correspondiente y si esta en favs.
        }

        public async Task Update(int id, CreateAndUpdateContact dto)
        {
            int contac_id = id;
            var contacItem = await _context.Contacts.FirstOrDefaultAsync(c => c.Id== id);

            if (contacItem != null)
            {
                var contac_map = _mapper.Map<Contact>(dto);
                contacItem.Name = contac_map.Name;
                contacItem.CelularNumber = contac_map.CelularNumber;
                contacItem.TelephoneNumber = contac_map.TelephoneNumber;
                contacItem.Favorite = contac_map.Favorite;
                
                /*wait _context.Contacts.AddAsync(_mapper.Map<Contact>(dto));*/
                await _context.SaveChangesAsync();
            }
                                    
        }
        
    }
}

        //        public void Create(CreateAndUpdateContact dto, int id)
        //        {
        //            Contact contact = _mapper.Map<Contact>(dto);
        //            contact.UserId = id;
        //            _context.Contacts.Add(contact);
        //            _context.SaveChanges();

        //        }

        //        public void Delete(int id)
        //        {
        //            _context.Contacts.Remove(_context.Contacts.Single(c => c.Id == id));      
        //            _context.SaveChanges();

        //        }

        //        public List<Contact> GetAllByUser(int id)
        //        {
        //            return _context.Contacts.Where(c => c.User.Id == id).ToList();
        //        }

        //        public void Update(CreateAndUpdateContact dto)
        //        {

        //            _context.Contacts.Update(_mapper.Map<Contact>(dto));
        //            _context.SaveChanges();

        //        }
        //        //public void Update(CreateAndUpdateContact dto)
        //        //{
        //        //    Contact New_contact = _mapper.Map<Contact>(dto);

        //        //    _context.Contacts.Update(_mapper.Map<Contact>(New_contact));
        //        //    _context.SaveChanges();
        //        //}
        //    }

    
