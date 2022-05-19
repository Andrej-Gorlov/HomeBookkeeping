using AutoMapper;
using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HomeBookkeepingWebApi.DAL.Repository
{
    public class СreditСardRepository : IСreditСardRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public СreditСardRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<СreditСardDTO> CreateAsync(СreditСardDTO entity)
        {
            СreditСard creditСard = _mapper.Map<СreditСardDTO, СreditСard>(entity);
            _db.СreditСard.Add(creditСard);
            await _db.SaveChangesAsync();
            return _mapper.Map<СreditСard, СreditСardDTO>(creditСard);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                СreditСard creditСard = await _db.СreditСard.FirstOrDefaultAsync(x => x.СreditСardId == id);
                if (creditСard is null) 
                {
                    return false;
                }
                _db.СreditСard.Remove(creditСard);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<СreditСardDTO> EnrollmentAsync(string nameBank, string number, decimal sum)
        {
            СreditСard? creditСard = await _db.СreditСard
                .FirstOrDefaultAsync(x => x.BankName == nameBank && x.Number.Replace(" ", "") == number.Replace(" ", ""));

            if (creditСard!=null)
            {
                creditСard.Sum += sum;
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<СreditСard, СreditСardDTO>(creditСard);
        }
        public async Task<IEnumerable<СreditСardDTO>> GetAsync() =>

            _mapper.Map<List<СreditСardDTO>>(await _db.СreditСard.ToListAsync());
        public async Task<IEnumerable<СreditСardDTO>> GetAsync(string fullName) =>

            _mapper.Map<List<СreditСardDTO>>(await _db.СreditСard
                .Where(x => x.UserFullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""))
                .ToListAsync());
        public async Task<СreditСardDTO> GetByIdAsync(int id) =>
            
            _mapper.Map<СreditСardDTO>(await _db.СreditСard
                .Where(x => x.СreditСardId == id)
                .FirstOrDefaultAsync());
        public async Task<СreditСardDTO> UpdateAsync(СreditСardDTO entity)
        {
            СreditСard creditСard = _mapper.Map<СreditСardDTO, СreditСard>(entity);
            if (await _db.СreditСard.AsNoTracking().FirstOrDefaultAsync(x => x.СreditСardId == entity.СreditСardId) is null)
            {
                throw new NullReferenceException("Попытка обновить объект, которого нет в хранилище.");
            }
            _db.СreditСard.Update(creditСard);
            await _db.SaveChangesAsync();
            return _mapper.Map<СreditСard, СreditСardDTO>(creditСard);
        }
    }
}
