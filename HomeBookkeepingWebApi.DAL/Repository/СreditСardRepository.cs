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
    public class СreditСardRepository : IСreditСardRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public СreditСardRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<СreditСardDTO> Create(СreditСardDTO entity)
        {
            СreditСard creditСard = _mapper.Map<СreditСardDTO, СreditСard>(entity);
            _db.СreditСard.Add(creditСard);
            await _db.SaveChangesAsync();
            return _mapper.Map<СreditСard, СreditСardDTO>(creditСard);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                СreditСard creditСard = await _db.СreditСard.FirstOrDefaultAsync(x => x.СreditСardId == id);
                if (creditСard == null) return false;
                _db.СreditСard.Remove(creditСard);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<СreditСardDTO> Enrollment(string nameBank, string number, decimal sum)
        {
            СreditСard creditСard = await _db.СreditСard.FirstOrDefaultAsync(x => x.BankName == nameBank && x.Number.Replace(" ", "") == number.Replace(" ", ""));
            creditСard.Sum += sum;
            await _db.SaveChangesAsync();
            return _mapper.Map<СreditСard, СreditСardDTO>(creditСard);
        }

        public async Task<IEnumerable<СreditСardDTO>> Get()
        {
            List<СreditСard> creditСardList = await _db.СreditСard.ToListAsync();
            return _mapper.Map<List<СreditСardDTO>>(creditСardList);
        }

        public async Task<IEnumerable<СreditСardDTO>> Get(string fullName)
        {
            List<СreditСard> creditСardList = await _db.СreditСard.Where(x=>x.UserFullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", "")). ToListAsync();
            return _mapper.Map<List<СreditСardDTO>>(creditСardList);
        }

        public async Task<СreditСardDTO> GetById(int id)
        {
            СreditСard creditСard = await _db.СreditСard.Where(x => x.СreditСardId == id).FirstOrDefaultAsync();
            return _mapper.Map<СreditСardDTO>(creditСard);
        }

        public async Task<СreditСardDTO> Update(СreditСardDTO entity)
        {
            СreditСard creditСard = _mapper.Map<СreditСardDTO, СreditСard>(entity);
            _db.СreditСard.Update(creditСard);
            await _db.SaveChangesAsync();
            return _mapper.Map<СreditСard, СreditСardDTO>(creditСard);
        }
    }
}
