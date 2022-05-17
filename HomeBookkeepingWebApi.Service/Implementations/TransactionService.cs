using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Response;
using HomeBookkeepingWebApi.Service.Interfaces;

namespace HomeBookkeepingWebApi.Service.Implementations
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRep;
        public TransactionService(ITransactionRepository transactionRep) => _transactionRep = transactionRep;
        public async Task<IBaseResponse<TransactionDTO>> AddServiceAsync(TransactionDTO entity)
        {
            var baseResponse = new BaseResponse<TransactionDTO>();
            TransactionDTO model = await _transactionRep.AddAsync(entity);
            baseResponse.DisplayMessage = "Транзакции создана";
            baseResponse.Result = model;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _transactionRep.DeleteAsync(id);
            if (IsSuccess)
            {
                baseResponse.DisplayMessage = "Транзакция удалена.";
            }
            if (!IsSuccess)
            {
                baseResponse.DisplayMessage = $"Транзакция c указанным id: {id} не найдена.";
            }
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(DateTime data)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _transactionRep.DeleteAsync(data);
            if (IsSuccess)
            {
                baseResponse.DisplayMessage = "Транзакция удалена.";
            }
            if (!IsSuccess)
            {
                baseResponse.DisplayMessage = $"Транзакция c указанной датой: {data} не найдена.";
            }
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<IEnumerable<TransactionDTO>>> GetServiceAsync()
        {
            var baseResponse = new BaseResponse<IEnumerable<TransactionDTO>>();
            IEnumerable<TransactionDTO> transactionDTO = await _transactionRep.GetAsync();
            if (transactionDTO is null)
            {
                baseResponse.DisplayMessage = "Список всех транзакций пуст.";
            }
            else
            {
                baseResponse.DisplayMessage = "Список всех транзакций.";
            }
            baseResponse.Result = transactionDTO;
            return baseResponse;
        }
        public async Task<IBaseResponse<TransactionDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<TransactionDTO>();
            TransactionDTO model = await _transactionRep.GetByIdAsync(id);
            if (model is null)
            {
                baseResponse.DisplayMessage = $"Транзакция под id [{id}] не найдена";
            }
            else
            {
                baseResponse.DisplayMessage = $"Вывод транзакций под id [{id}]";
            }
            baseResponse.Result = model;
            return baseResponse;
        }
    }
}
