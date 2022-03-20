using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.Entity.TemporaryData;
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

        public async Task<IBaseResponse<IEnumerable<ReportCategory>>> Service_ReportByCategoryAllYears(string category)
        {
            var baseResponse = new BaseResponse<IEnumerable<ReportCategory>>();
            IEnumerable<ReportCategory> temporaryDRC_DTO = await _reportRep.ReportByCategoryAllYears(category);
            if (temporaryDRC_DTO == null)
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за все года пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за все года.";
            baseResponse.Result = temporaryDRC_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<ReportCategory>>> Service_ReportByCategoryYaer(string category, int year)
        {
            var baseResponse = new BaseResponse<IEnumerable<ReportCategory>>();
            IEnumerable<ReportCategory> temporaryDRC_DTO = await _reportRep.ReportByCategoryYaer(category, year);
            if (temporaryDRC_DTO == null)
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за год [{year}].";
            baseResponse.Result = temporaryDRC_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<Report>>> Service_FullReport()
        {
            var baseResponse = new BaseResponse<IEnumerable<Report>>();
            IEnumerable<Report> temporaryDRT_DTO = await _reportRep.FullReport();
            if (temporaryDRT_DTO == null)
                baseResponse.DisplayMessage = $"Список всех расходов пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов.";
            baseResponse.Result = temporaryDRT_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<ReportCategory>>> Service_ReportByCategoryNameUserYear(string category, string fullName, int year)
        {
            var baseResponse = new BaseResponse<IEnumerable<ReportCategory>>();
            IEnumerable<ReportCategory> temporaryDRT_DTO = await _reportRep.ReportByCategoryNameUserYear(category, fullName, year);
            if (temporaryDRT_DTO == null)
                baseResponse.DisplayMessage = $"Список расходов по категории [{category}] пользователя [{fullName}] за год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов по категории [{category}] пользователя [{fullName}] за год [{year}].";
            baseResponse.Result = temporaryDRT_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<ReportCategory>> Service_ReportByCategoryNameUserYearMonth(string category, string fullName, int year, string month)
        {
            var baseResponse = new BaseResponse<ReportCategory>();
            ReportCategory model = await _reportRep.ReportByCategoryNameUserYearMonth(category, fullName, year, month);
            if (model.Sum == 0)
                baseResponse.DisplayMessage = $"Пользователь [{fullName}] за [{month}]/[{year}] г. по категории [{category}] не совершал расходов.";
            if (model == null)
                baseResponse.DisplayMessage = $"Отчёт по категории [{category}] пользователя [{fullName}] за год [{year}] и месеца [{month}] не найден.";
            else
                baseResponse.DisplayMessage = $"Отчёт по категории [{category}] пользователя [{fullName}] за год [{year}] и месеца [{month}]";
            baseResponse.Result = model;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<Report>>> Service_ReportAllYearsNameUser(string fullName)
        {
            var baseResponse = new BaseResponse<IEnumerable<Report>>();
            IEnumerable<Report> temporaryDRT_DTO = await _reportRep.ReportAllYearsNameUser(fullName);
            if (temporaryDRT_DTO == null)
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все года пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все года.";
            baseResponse.Result = temporaryDRT_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<Report>>> Service_ReportByNameUserYear(string fullName, int year)
        {
            var baseResponse = new BaseResponse<IEnumerable<Report>>();
            IEnumerable<Report> temporaryDRT_DTO = await _reportRep.ReportByNameUserYear(fullName, year);
            if (temporaryDRT_DTO == null)
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все год [{year}].";
            baseResponse.Result = temporaryDRT_DTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<Report>> Service_ReportByNameUserYearMonth(string fullName, int year, string month)
        {
            var baseResponse = new BaseResponse<Report>();
            Report model = await _reportRep.ReportByNameUserYearMonth(fullName, year, month);
            if (model.Sum == 0)
                baseResponse.DisplayMessage = $"Пользователь [{fullName}] за [{month}]/[{year}] г. не совершал расходов.";
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

        public async Task<IBaseResponse<List<string>>> Service_GetAllFullNameUser()
        {
            var baseResponse = new BaseResponse<List<string>>();
            var model = await _reportRep.GetAllFullNameUser();
            if (model.Count() > 0)
                baseResponse.DisplayMessage = $"Список пользователей.";
            else
                baseResponse.DisplayMessage = $"Список пользователей пуст.";
            baseResponse.Result = model.ToList();
            return baseResponse;
        }
    }
}
