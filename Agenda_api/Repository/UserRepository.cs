


using Agenda_api.Data;
using Agenda_api.Entities;
using Agenda_api.Models.DTOs;
using Agenda_api.Repository.Interfaces;
using AutoMapper;

namespace Agenda_api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AgendaApiContext _context;                               // Inyecto el Context y el Mapper
        private readonly IMapper _mapper;
        public UserRepository(AgendaApiContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;                     
        }

        public void Create(CreateAndUpdateUser dto)
        {
             _context.Users.Add(_mapper.Map<User>(dto));
        }

        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == id));
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }

        public void Update(CreateAndUpdateUser dto)
        {
            _context.Users.Add(_mapper.Map<User>(dto));
        }

        public User? Validate(AutenticationRequestBody aurhRequestBody)
        {
            return  _context.Users.FirstOrDefault(p => p.UserName == aurhRequestBody.UserName && p.Password == aurhRequestBody.Password);
        }
    }
}
