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
    public class СreditСardService : IСreditСardService
    {
        private IСreditСardRepository _creditСR;
        public СreditСardService(IСreditСardRepository creditСR) => _creditСR = creditСR;

        public async Task<IBaseResponse<СreditСardDTO>> ServiceCreate(СreditСardDTO entity)
        {
            var baseResponse = new BaseResponse<СreditСardDTO>();
            СreditСardDTO model = await _creditСR.Create(entity);
            baseResponse.DisplayMessage = "Игра создана";
            baseResponse.Result = model;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> ServiceDelete(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _creditСR.Delete(id);
            if (IsSuccess) baseResponse.DisplayMessage = "Кредитная карта удалена.";
            if (!IsSuccess) baseResponse.DisplayMessage = $"Кредитная карта c указанным id: {id} не найдена.";
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<СreditСardDTO>> ServiceEnrollment(string nameBank, string number, decimal sum)
        {
            var baseResponse = new BaseResponse<СreditСardDTO>();
            СreditСardDTO model = await _creditСR.Enrollment(nameBank, number, sum);
            baseResponse.DisplayMessage = $"Денежные средства зачислены на карту №{number}";
            baseResponse.Result = model;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<СreditСardDTO>>> ServiceGet()
        {
            var baseResponse = new BaseResponse<IEnumerable<СreditСardDTO>>();
            IEnumerable<СreditСardDTO> creditСardsDTO = await _creditСR.Get();
            if (creditСardsDTO == null) baseResponse.DisplayMessage = "Список всех кредитных карт пуст.";
            else baseResponse.DisplayMessage = "Список всех кредитных карт.";
            baseResponse.Result = creditСardsDTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<СreditСardDTO>>> ServiceGet(string fullName)
        {
            var baseResponse = new BaseResponse<IEnumerable<СreditСardDTO>>();
            IEnumerable<СreditСardDTO> creditСardsDTO = await _creditСR.Get(fullName);
            if (creditСardsDTO == null) baseResponse.DisplayMessage = $"Список всех кредитных карт принадлежащему пользователю {fullName} пуст.";
            else baseResponse.DisplayMessage = $"Список всех кредитных карт принадлежащему пользователю {fullName}.";
            baseResponse.Result = creditСardsDTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<СreditСardDTO>> ServiceGetById(int id)
        {
            var baseResponse = new BaseResponse<СreditСardDTO>();
            СreditСardDTO model = await _creditСR.GetById(id);
            if (model == null) baseResponse.DisplayMessage = $"Кредитная карта под id [{id}] не найдена";
            else baseResponse.DisplayMessage = $"Вывод кредитной карты под id [{id}]";
            baseResponse.Result = model;
            return baseResponse;
        }

        public async Task<IBaseResponse<СreditСardDTO>> ServiceUpdate(СreditСardDTO entity)
        {
            var baseResponse = new BaseResponse<СreditСardDTO>();
            СreditСardDTO model = await _creditСR.Update(entity);
            baseResponse.DisplayMessage = "Кредитная карта обновилась.";
            baseResponse.Result = model;
            return baseResponse;
        }
    }
}
