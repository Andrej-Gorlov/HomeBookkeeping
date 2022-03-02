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
    public class ReportService : IReportService
    {
        readonly IReportRepository _reportRep;
        public ReportService(IReportRepository reportRep) => _reportRep = reportRep;

        public async Task<IBaseResponse<IEnumerable<TemporaryData_ReportCategotyDTO>>> Service_ExpensCategoryFullYaer(string category)
        {
            var baseResponse = new BaseResponse<IEnumerable<TemporaryData_ReportCategotyDTO>>();
            IEnumerable<TemporaryData_ReportCategotyDTO> temporaryDRC_DTO = await _reportRep.ExpensCategoryFullYaer(category);
            if (temporaryDRC_DTO == null)
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за все года пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за все года.";
            baseResponse.Result = temporaryDRC_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<TemporaryData_ReportCategotyDTO>>> Service_ExpensCategoryYaer(string category, int year)
        {
            var baseResponse = new BaseResponse<IEnumerable<TemporaryData_ReportCategotyDTO>>();
            IEnumerable<TemporaryData_ReportCategotyDTO> temporaryDRC_DTO = await _reportRep.ExpensCategoryYaer(category, year);
            if (temporaryDRC_DTO == null)
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за год [{year}].";
            baseResponse.Result = temporaryDRC_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<TemporaryData_ReportTimeDTO>>> Service_ExpensFull()
        {
            var baseResponse = new BaseResponse<IEnumerable<TemporaryData_ReportTimeDTO>>();
            IEnumerable<TemporaryData_ReportTimeDTO> temporaryDRT_DTO = await _reportRep.ExpensFull();
            if (temporaryDRT_DTO == null)
                baseResponse.DisplayMessage = $"Список всех расходов пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов.";
            baseResponse.Result = temporaryDRT_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<TemporaryData_ReportCategotyDTO>>> Service_ExpensNameCategoryYear(string category, string fullName, int year)
        {
            var baseResponse = new BaseResponse<IEnumerable<TemporaryData_ReportCategotyDTO>>();
            IEnumerable<TemporaryData_ReportCategotyDTO> temporaryDRT_DTO = await _reportRep.ExpensNameCategoryYear(category, fullName, year);
            if (temporaryDRT_DTO == null)
                baseResponse.DisplayMessage = $"Список расходов по категории [{category}] пользователя [{fullName}] за год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов по категории [{category}] пользователя [{fullName}] за год [{year}].";
            baseResponse.Result = temporaryDRT_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<TemporaryData_ReportCategotyDTO>> Service_ExpensNameCategoryYearMonth(string category, string fullName, int year, string month)
        {
            var baseResponse = new BaseResponse<TemporaryData_ReportCategotyDTO>();
            TemporaryData_ReportCategotyDTO model = await _reportRep.ExpensNameCategoryYearMonth(category, fullName, year, month);
            if (model == null)
                baseResponse.DisplayMessage = $"Отчёт по категории [{category}] пользователя [{fullName}] за год [{year}] и месеца [{month}] не найден.";
            else
                baseResponse.DisplayMessage = $"Отчёт по категории [{category}] пользователя [{fullName}] за год [{year}] и месеца [{month}]";
            baseResponse.Result = model;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<TemporaryData_ReportTimeDTO>>> Service_ExpensNameFullYear(string fullName)
        {
            var baseResponse = new BaseResponse<IEnumerable<TemporaryData_ReportTimeDTO>>();
            IEnumerable<TemporaryData_ReportTimeDTO> temporaryDRT_DTO = await _reportRep.ExpensNameFullYear(fullName);
            if (temporaryDRT_DTO == null)
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все года пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все года.";
            baseResponse.Result = temporaryDRT_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<List<TemporaryData_ReportTimeDTO>>> Service_ExpensNameYear(string fullName, int year)
        {
            var baseResponse = new BaseResponse<List<TemporaryData_ReportTimeDTO>>();
            List<TemporaryData_ReportTimeDTO> temporaryDRT_DTO = await _reportRep.ExpensNameYear(fullName, year);
            if (temporaryDRT_DTO == null)
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все год [{year}].";
            baseResponse.Result = temporaryDRT_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<TemporaryData_ReportTimeDTO>> Service_ExpensNameYearMonth(string fullName, int year, string month)
        {
            var baseResponse = new BaseResponse<TemporaryData_ReportTimeDTO>();
            TemporaryData_ReportTimeDTO model = await _reportRep.ExpensNameYearMonth(fullName, year, month);
            if (model == null)
                baseResponse.DisplayMessage = $"Отчёт пользователя [{fullName}] за год [{year}] и месеца [{month}] не найден.";
            else
                baseResponse.DisplayMessage = $"Отчёт пользователя [{fullName}] за год [{year}] и месеца [{month}]";
            baseResponse.Result = model;
            return baseResponse;
        }

        public async Task<IBaseResponse<List<string>>> Service_GetAllCategory()
        {
            var baseResponse = new BaseResponse<List<string>>();
            var model = await _reportRep.GetAllCategory();
            if (model.Count() > 0)
                baseResponse.DisplayMessage = $"Список категорий.";
            else
                baseResponse.DisplayMessage = $"Список категорий пуст.";
            baseResponse.Result = model.ToList();
            return baseResponse;
        }
    }
}
