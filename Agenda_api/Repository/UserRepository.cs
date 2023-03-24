


using Agenda_api.Data;
using Agenda_api.Entities;
using Agenda_api.Models.DTOs;
using Agenda_api.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

        //public async Task Archive(int id)
        //{
        //    User user = _context.Users.SingleOrDefault(u => u.Id == id);             //Guardo el User a cambiar en una variable
        //    user.State = Models.Enum.State.Archived;                                // Le cambio el valor de active a archive       
        //    _context.Update(user);                                                  //Actualizo el user
        //     await _context.SaveChangesAsync();
        //}

        public async Task Create(CreateAndUpdateUser dto)
        {
            _context.Users.Add(_mapper.Map<User>(dto));    //mapeado de dto a user.
           
             await _context.SaveChangesAsync();
            
        }
        public async Task Update(int id_user, CreateAndUpdateUser dto)
        {

            var id = id_user; 
            var userItem = _context.Users.FirstOrDefaultAsync(u => u.Id == id);    

            if (userItem != null)
            {
                await _context.Users.AddAsync(_mapper.Map<User>(dto));   // aca mapeo el dto a user y se lo agrego al context.
                await _context.SaveChangesAsync();
            }
            

        }

        //public async Task Delete(int id)
        //{
        //    _context.Users.Remove(_context.Users.Single(u => u.Id == id));
        //    await _context.SaveChangesAsync();
        //}

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();     //OK
        }

        public async Task<User> GetById(int userId)
        {
            return await _context.Users.FindAsync(userId);   //
        }



        public async Task<User?> Validate(AutenticationRequestBody authRequestBody)
        {
           return await _context.Users.FirstAsync(p => p.UserName == authRequestBody.UserName && p.Password == authRequestBody.Password);
        }

        public async Task Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == id));
            await _context.SaveChangesAsync();
        }

        public async Task Archive(int id)
        {
            User user = _context.Users.SingleOrDefault(u => u.Id == id);             //Guardo el User a cambiar en una variable
           user.State = Models.Enum.State.Archived;                                // Le cambio el valor de active a archive       
           _context.Update(user);                                                  //Actualizo el user
           await _context.SaveChangesAsync();

        }



        //    public void Create(CreateAndUpdateUser dto)
        //    {
        //        _context.Users.Add(_mapper.Map<User>(dto));
        //        _context.SaveChanges();
        //    }

        //    public void Delete(int id)
        //    {
        //        _context.Users.Remove(_context.Users.Single(u => u.Id == id));
        //        _context.SaveChanges();
        //    }

        //    //public List<User> GetAll()
        //    //{
        //    //    return _context.Users.ToList();
        //    //}



        //    public User? GetById(int userId)
        //    {
        //        return _context.Users.SingleOrDefault(u => u.Id == userId);
        //    }

        //    public void Update(CreateAndUpdateUser dto)
        //    {
        //        _context.Users.Add(_mapper.Map<User>(dto));
        //        _context.SaveChanges();
        //    }

        //    public User? Validate(AutenticationRequestBody authRequestBody)
        //    {
        //        return  _context.Users.FirstOrDefault(p => p.UserName == authRequestBody.UserName && p.Password == authRequestBody.Password);
        //    }
        //    public void Archive(int id)
        //    {
        //        User user = _context.Users.SingleOrDefault(u => u.Id == id);
        //        if (user != null)
        //        {
        //            user.State = Models.Enum.State.Archived;
        //            _context.Update(user);
        //        }
        //        _context.SaveChanges();
        //    }


        //    public void Update(User dto)
        //    {
        //        _context.Users.Add(_mapper.Map<User>(dto));
        //        _context.SaveChanges();
        //    }

        //}
    }
}
