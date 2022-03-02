using HomeBookkeepingWebApi.Domain.DTO;
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
        Task<IBaseResponse<List<TemporaryData_ReportTimeDTO>>> Service_ExpensNameYear(string fullName, int year);
        Task<IBaseResponse<TemporaryData_ReportTimeDTO>> Service_ExpensNameYearMonth(string fullName, int year, string month);


        Task<IBaseResponse<IEnumerable<TemporaryData_ReportCategotyDTO>>> Service_ExpensNameCategoryYear(string category, string fullName, int year);
        Task<IBaseResponse<TemporaryData_ReportCategotyDTO>> Service_ExpensNameCategoryYearMonth(string category, string fullName, int year, string month);


        Task<IBaseResponse<IEnumerable<TemporaryData_ReportTimeDTO>>> Service_ExpensFull();
        Task<IBaseResponse<IEnumerable<TemporaryData_ReportTimeDTO>>> Service_ExpensNameFullYear(string fullName);


        Task<IBaseResponse<IEnumerable<TemporaryData_ReportCategotyDTO>>> Service_ExpensCategoryFullYaer(string category);
        Task<IBaseResponse<IEnumerable<TemporaryData_ReportCategotyDTO>>> Service_ExpensCategoryYaer(string category, int year);
    }
}
