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
        Task<IBaseResponse<List<string>>> ServiceGetAllCategory();
        Task<IBaseResponse<List<string>>> ServiceGetAllFullNameUser();
        Task<IBaseResponse<IEnumerable<Report>>> ServiceReportByNameUserYear(string fullName, int year);
        Task<IBaseResponse<Report>> ServiceReportByNameUserYearMonth(string fullName, int year, string month);


        Task<IBaseResponse<IEnumerable<ReportCategory>>> ServiceReportByCategoryNameUserYear(string category, string fullName, int year);
        Task<IBaseResponse<ReportCategory>> ServiceReportByCategoryNameUserYearMonth(string category, string fullName, int year, string month);


        Task<IBaseResponse<IEnumerable<Report>>> ServiceFullReport();
        Task<IBaseResponse<IEnumerable<Report>>> ServiceReportAllYearsNameUser(string fullName);


        Task<IBaseResponse<IEnumerable<ReportCategory>>> ServiceReportByCategoryAllYears(string category);
        Task<IBaseResponse<IEnumerable<ReportCategory>>> ServiceReportByCategoryYaer(string category, int year);
    }
}
