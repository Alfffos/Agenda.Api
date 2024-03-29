﻿


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


        public async Task Create(CreateAndUpdateUser dto)
        {
            _context.Users.Add(_mapper.Map<User>(dto));    //mapeado de dto a user.
           
             await _context.SaveChangesAsync();
            
        }
        public async Task Update(int id_user, CreateAndUpdateUser dto)
        {

            var id = id_user; 
            var userItem =  await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            var user_update = dto;

            if (userItem != null)
            {
                userItem.Name = user_update.Name;
                userItem.LastName = user_update.LastName;
                userItem.UserName = user_update.UserName;
                userItem.Email = user_update.Email;
                userItem.Password = user_update.Password;

                await _context.SaveChangesAsync();
            }
            

        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();     
        }

        public async Task<User> GetById(int userId)
        {
            return await _context.Users.FindAsync(userId);   
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
            User user = _context.Users.SingleOrDefault(u => u.Id == id);           
           user.State = Models.Enum.State.Archived;                                       
           _context.Update(user);                                                  
           await _context.SaveChangesAsync();

        }
    }
}
