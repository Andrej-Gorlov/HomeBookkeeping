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
        Task<IBaseResponse<IEnumerable<TemporaryDataReportTime>>> Service_ExpensNameYear(string fullName, int year);
        Task<IBaseResponse<TemporaryDataReportTime>> Service_ExpensNameYearMonth(string fullName, int year, string month);


        Task<IBaseResponse<IEnumerable<TemporaryDataReportCategoty>>> Service_ExpensNameCategoryYear(string category, string fullName, int year);
        Task<IBaseResponse<TemporaryDataReportCategoty>> Service_ExpensNameCategoryYearMonth(string category, string fullName, int year, string month);


        Task<IBaseResponse<IEnumerable<TemporaryDataReportTime>>> Service_ExpensFull();
        Task<IBaseResponse<IEnumerable<TemporaryDataReportTime>>> Service_ExpensNameFullYear(string fullName);


        Task<IBaseResponse<IEnumerable<TemporaryDataReportCategoty>>> Service_ExpensCategoryFullYaer(string category);
        Task<IBaseResponse<IEnumerable<TemporaryDataReportCategoty>>> Service_ExpensCategoryYaer(string category, int year);
    }
}
