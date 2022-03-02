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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public TransactionRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }



        public async Task<TransactionDTO> Add(TransactionDTO entity)
        {
            Transaction transaction = _mapper.Map<TransactionDTO, Transaction>(entity);

            var user =await _db.User.FirstOrDefaultAsync(
                x => x.FullName.ToUpper().Replace(" ", "") == transaction.UserFullName.ToUpper().Replace(" ", ""));

            var card = await _db.СreditСard.FirstOrDefaultAsync(
                x => x.UserFullName.ToUpper().Replace(" ", "") == transaction.UserFullName.ToUpper().Replace(" ", "")
                && x.Number.ToUpper().Replace(" ", "") == transaction.NumberCardUser.ToUpper().Replace(" ", ""));

            if (user == null || card == null) throw new NotImplementedException();

            _db.Transaction.Add(transaction);

            card.Sum -= transaction.Sum;

            await _db.SaveChangesAsync();
            return _mapper.Map<Transaction, TransactionDTO>(transaction);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Transaction transaction = await _db.Transaction.FirstOrDefaultAsync(x => x.Id == id);
                if (transaction == null) return false;

                var card = await _db.СreditСard.FirstOrDefaultAsync(
                    x => x.UserFullName.ToUpper().Replace(" ", "") == transaction.UserFullName.ToUpper().Replace(" ", "")
                    && x.Number.ToUpper().Replace(" ", "") == transaction.NumberCardUser.ToUpper().Replace(" ", ""));

                card.Sum += transaction.Sum;

                _db.Transaction.Remove(transaction);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(DateTime data)
        {
            try
            {
                Transaction transaction = await _db.Transaction.FirstOrDefaultAsync(
                            x => x.DateOperations.Year == data.Year &&
                            x.DateOperations.Month == data.Month &&
                            x.DateOperations.Day == data.Day &&
                            x.DateOperations.Hour == data.Hour &&
                            x.DateOperations.Minute == data.Minute &&
                            x.DateOperations.Second == data.Second);
                if (transaction == null) return false;

                var card = await _db.СreditСard.FirstOrDefaultAsync(
                    x => x.UserFullName.ToUpper().Replace(" ", "") == transaction.UserFullName.ToUpper().Replace(" ", "")
                    && x.Number.ToUpper().Replace(" ", "") == transaction.NumberCardUser.ToUpper().Replace(" ", ""));

                card.Sum += transaction.Sum;

                _db.Transaction.Remove(transaction);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<TransactionDTO>> Get()
        {
            List<Transaction> transactionList = await _db.Transaction.ToListAsync();
            return _mapper.Map<List<TransactionDTO>>(transactionList);
        }
    }
}
