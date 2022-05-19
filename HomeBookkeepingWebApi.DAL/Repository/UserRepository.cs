using AutoMapper;
using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<UserDTO> CreateAsync(UserDTO entity)
        {
            User user = _mapper.Map<UserDTO, User>(entity);
            _db.User.Add(user);
            await _db.SaveChangesAsync();
            return _mapper.Map<User, UserDTO>(user);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                User? user = await _db.User.Include(s => s.СreditСards).FirstOrDefaultAsync(x => x.UserId == id);
                if (user is null)
                {
                    return false;
                }
                _db.User.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<UserDTO>> GetAsync() =>

            _mapper.Map<List<UserDTO>>(await _db.User.Include(x => x.СreditСards).ToListAsync());
        public async Task<UserDTO> GetByFullNameAsync(string fullName) =>

            _mapper.Map<UserDTO>(await _db.User
                .Where(x => x.FullName == fullName)
                .Include(s => s.СreditСards)
                .FirstOrDefaultAsync());
        public async Task<UserDTO> GetByIdAsync(int id) =>
            
            _mapper.Map<UserDTO>(await _db.User
                .Where(x => x.UserId == id)
                .Include(s => s.СreditСards)
                .FirstOrDefaultAsync());
        public async Task<UserDTO> UpdateAsync(UserDTO entity)
        {
            User user = _mapper.Map<UserDTO, User>(entity);
            if (await _db.User.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == entity.UserId) is null)
            {
                throw new NullReferenceException("Попытка обновить объект, которого нет в хранилище.");
            }
            _db.User.Update(user);
            await _db.SaveChangesAsync();
            return _mapper.Map<User, UserDTO>(user);
        }
    }
}
