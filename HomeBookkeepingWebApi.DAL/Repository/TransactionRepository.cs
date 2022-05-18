using AutoMapper;
using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HomeBookkeepingWebApi.DAL.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public TransactionRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<TransactionDTO> AddAsync(TransactionDTO entity)
        {
            Transaction transaction = _mapper.Map<TransactionDTO, Transaction>(entity);

            var card = await _db.СreditСard.FirstOrDefaultAsync(
                x => x.UserFullName.ToUpper().Replace(" ", "") == transaction.UserFullName.ToUpper().Replace(" ", "")
                && x.Number.ToUpper().Replace(" ", "") == transaction.NumberCardUser.ToUpper().Replace(" ", ""));

            _db.Transaction.Add(transaction);

            card.Sum -= transaction.Sum;

            await _db.SaveChangesAsync();
            return _mapper.Map<Transaction, TransactionDTO>(transaction);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Transaction? transaction = await _db.Transaction.FirstOrDefaultAsync(x => x.Id == id);
                if (transaction is null)
                {
                    return false;
                }
                var card = await _db.СreditСard.FirstOrDefaultAsync(
                    x => x.UserFullName.ToUpper().Replace(" ", "") == transaction.UserFullName.ToUpper().Replace(" ", "")
                    && x.Number.ToUpper().Replace(" ", "") == transaction.NumberCardUser.ToUpper().Replace(" ", ""));
                if (card != null)
                {
                    card.Sum += transaction.Sum;
                }
                _db.Transaction.Remove(transaction);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(DateTime data)
        {
            try
            {
                Transaction? transaction = await _db.Transaction.FirstOrDefaultAsync(
                            x => x.DateOperations.Year == data.Year &&
                            x.DateOperations.Month == data.Month &&
                            x.DateOperations.Day == data.Day &&
                            x.DateOperations.Hour == data.Hour &&
                            x.DateOperations.Minute == data.Minute &&
                            x.DateOperations.Second == data.Second);
                if (transaction is null)
                {
                    return false;
                }
                var card = await _db.СreditСard.FirstOrDefaultAsync(
                    x => x.UserFullName.ToUpper().Replace(" ", "") == transaction.UserFullName.ToUpper().Replace(" ", "")
                    && x.Number.ToUpper().Replace(" ", "") == transaction.NumberCardUser.ToUpper().Replace(" ", ""));
                if (card!=null)
                {
                    card.Sum += transaction.Sum;
                }
                _db.Transaction.Remove(transaction);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<TransactionDTO>> GetAsync() =>
            
            _mapper.Map<List<TransactionDTO>>(await _db.Transaction.ToListAsync());
        public async Task<TransactionDTO> GetByIdAsync(int id) =>
           
            _mapper.Map<TransactionDTO>(await _db.Transaction.FirstOrDefaultAsync(x => x.Id == id));
    }
}
