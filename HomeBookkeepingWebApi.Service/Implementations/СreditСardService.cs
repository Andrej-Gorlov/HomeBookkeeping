using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Response;
using HomeBookkeepingWebApi.Service.Interfaces;

namespace HomeBookkeepingWebApi.Service.Implementations
{
    public class СreditСardService : IСreditСardService
    {
        private IСreditСardRepository _creditСR;
        public СreditСardService(IСreditСardRepository creditСR) => _creditСR = creditСR;
        public async Task<IBaseResponse<СreditСardDTO>> CreateServiceAsync(СreditСardDTO entity)
        {
            var baseResponse = new BaseResponse<СreditСardDTO>();
            СreditСardDTO model = await _creditСR.CreateAsync(entity);
            baseResponse.DisplayMessage = "Кредитная карта создана";
            baseResponse.Result = model;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _creditСR.DeleteAsync(id);
            if (IsSuccess)
            {
                baseResponse.DisplayMessage = "Кредитная карта удалена.";
            }
            if (!IsSuccess)
            {
                baseResponse.DisplayMessage = $"Кредитная карта c указанным id: {id} не найдена.";
            }
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<СreditСardDTO>> EnrollmentServiceAsync(string nameBank, string number, decimal sum)
        {
            var baseResponse = new BaseResponse<СreditСardDTO>();
            СreditСardDTO model = await _creditСR.EnrollmentAsync(nameBank, number, sum);
            baseResponse.DisplayMessage = $"Денежные средства зачислены на карту №{number}";
            baseResponse.Result = model;
            return baseResponse;
        }
        public async Task<IBaseResponse<IEnumerable<СreditСardDTO>>> GetServiceAsync()
        {
            var baseResponse = new BaseResponse<IEnumerable<СreditСardDTO>>();
            IEnumerable<СreditСardDTO> creditСardsDTO = await _creditСR.GetAsync();
            if (creditСardsDTO is null)
            {
                baseResponse.DisplayMessage = "Список всех кредитных карт пуст.";
            }
            else
            {
                baseResponse.DisplayMessage = "Список всех кредитных карт.";
            }
            baseResponse.Result = creditСardsDTO;
            return baseResponse;
        }
        public async Task<IBaseResponse<IEnumerable<СreditСardDTO>>> GetServiceAsync(string fullName)
        {
            var baseResponse = new BaseResponse<IEnumerable<СreditСardDTO>>();
            IEnumerable<СreditСardDTO> creditСardsDTO = await _creditСR.GetAsync(fullName);
            if (creditСardsDTO is null)
            {
                baseResponse.DisplayMessage = $"Список всех кредитных карт принадлежащему пользователю {fullName} пуст.";
            }
            else
            {
                baseResponse.DisplayMessage = $"Список всех кредитных карт принадлежащему пользователю {fullName}.";
            }
            baseResponse.Result = creditСardsDTO;
            return baseResponse;
        }
        public async Task<IBaseResponse<СreditСardDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<СreditСardDTO>();
            СreditСardDTO model = await _creditСR.GetByIdAsync(id);
            if (model is null)
            {
                baseResponse.DisplayMessage = $"Кредитная карта под id [{id}] не найдена";
            }
            else
            {
                baseResponse.DisplayMessage = $"Вывод кредитной карты под id [{id}]";
            }
            baseResponse.Result = model;
            return baseResponse;
        }
        public async Task<IBaseResponse<СreditСardDTO>> UpdateServiceAsync(СreditСardDTO entity)
        {
            var baseResponse = new BaseResponse<СreditСardDTO>();
            СreditСardDTO model = await _creditСR.UpdateAsync(entity);
            baseResponse.DisplayMessage = "Кредитная карта обновилась.";
            baseResponse.Result = model;
            return baseResponse;
        }
    }
}
