using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Response;
using HomeBookkeepingWebApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Service.Implementations
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRep;
        public TransactionService(ITransactionRepository transactionRep) => _transactionRep = transactionRep;

        public async Task<IBaseResponse<TransactionDTO>> Service_Add(TransactionDTO entity)
        {
            var baseResponse = new BaseResponse<TransactionDTO>();
            TransactionDTO model = await _transactionRep.Add(entity);
            baseResponse.DisplayMessage = "Транзакции создана";
            baseResponse.Result = model;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> Service_Delete(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _transactionRep.Delete(id);
            if (IsSuccess) baseResponse.DisplayMessage = "Транзакция удалена.";
            if (!IsSuccess) baseResponse.DisplayMessage = $"Транзакция c указанным id: {id} не найдена.";
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> Service_Delete(DateTime data)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _transactionRep.Delete(data);
            if (IsSuccess) baseResponse.DisplayMessage = "Транзакция удалена.";
            if (!IsSuccess) baseResponse.DisplayMessage = $"Транзакция c указанной датой: {data} не найдена.";
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<TransactionDTO>>> Service_Get()
        {
            var baseResponse = new BaseResponse<IEnumerable<TransactionDTO>>();
            IEnumerable<TransactionDTO> transactionDTO = await _transactionRep.Get();
            if (transactionDTO == null) baseResponse.DisplayMessage = "Список всех транзакций пуст.";
            else baseResponse.DisplayMessage = "Список всех транзакций.";
            baseResponse.Result = transactionDTO;
            return baseResponse;
        }
    }
}
