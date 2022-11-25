


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
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == id));
            _context.SaveChanges();
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
            _context.SaveChanges();
        }

        public User? Validate(AutenticationRequestBody authRequestBody)
        {
            return  _context.Users.FirstOrDefault(p => p.UserName == authRequestBody.UserName && p.Password == authRequestBody.Password);
        }
        public void Archive(int id)
        {
            User user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.State = Models.Enum.State.Archived;
                _context.Update(user);
            }
            _context.SaveChanges();
        }
    }
}
