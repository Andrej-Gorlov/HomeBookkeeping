using AutoMapper;
using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public UserRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserDTO> Create(UserDTO entity)
        {
            User user = _mapper.Map<UserDTO, User>(entity);
            _db.User.Add(user);
            await _db.SaveChangesAsync();
            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                User user = await _db.User.Include(s => s.СreditСard).FirstOrDefaultAsync(x => x.UserId == id);
                if (user == null) return false;
                _db.User.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<UserDTO>> Get()
        {
            List<User> userList = await _db.User.Include(x => x.СreditСard).ToListAsync();
            return _mapper.Map<List<UserDTO>>(userList); ;
        }

        public async Task<UserDTO> GetByFullName(string fullName)
        {
            User user = await _db.User.Where(x => x.FullName == fullName).Include(s => s.СreditСard).FirstOrDefaultAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetById(int id)
        {
            User user = await _db.User.Where(x => x.UserId == id).Include(s => s.СreditСard).FirstOrDefaultAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> Update(UserDTO entity)
        {
            User user = _mapper.Map<UserDTO, User>(entity);
            _db.User.Update(user);
            await _db.SaveChangesAsync();
            return _mapper.Map<User, UserDTO>(user);
        }
    }
}
