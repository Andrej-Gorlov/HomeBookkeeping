using HomeBookkeepingWebApi.Domain.Entity.TemporaryData;
using HomeBookkeepingWebApi.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface IReportService
    {
        Task<IBaseResponse<List<string>>> Service_GetAllCategory();
        Task<IBaseResponse<List<string>>> Service_GetAllFullNameUser();
        Task<IBaseResponse<IEnumerable<Report>>> Service_ReportByNameUserYear(string fullName, int year);
        Task<IBaseResponse<Report>> Service_ReportByNameUserYearMonth(string fullName, int year, string month);


        Task<IBaseResponse<IEnumerable<ReportCategory>>> Service_ReportByCategoryNameUserYear(string category, string fullName, int year);
        Task<IBaseResponse<ReportCategory>> Service_ReportByCategoryNameUserYearMonth(string category, string fullName, int year, string month);


        Task<IBaseResponse<IEnumerable<Report>>> Service_FullReport();
        Task<IBaseResponse<IEnumerable<Report>>> Service_ReportAllYearsNameUser(string fullName);


        Task<IBaseResponse<IEnumerable<ReportCategory>>> Service_ReportByCategoryAllYears(string category);
        Task<IBaseResponse<IEnumerable<ReportCategory>>> Service_ReportByCategoryYaer(string category, int year);
    }
}
