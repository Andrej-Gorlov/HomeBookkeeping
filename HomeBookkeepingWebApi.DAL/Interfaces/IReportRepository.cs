using HomeBookkeepingWebApi.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.DAL.Interfaces
{
    public interface IReportRepository
    {
        Task<List<string>> GetAllCategory();
        // Отчёт определенного user за конкретный год
        Task<List<TemporaryData_ReportTimeDTO>> ExpensNameYear(string fullName, int year);
        // Отчёт определенного user за конкретный год и месяц 
        Task<TemporaryData_ReportTimeDTO> ExpensNameYearMonth(string fullName, int year, string month);


        // Отчёт категории определенного user за конкретный год
        Task<IEnumerable<TemporaryData_ReportCategotyDTO>> ExpensNameCategoryYear(string category, string fullName, int year);
        //Отчёт категории определенного user за конкретный год и месяц 
        Task<TemporaryData_ReportCategotyDTO> ExpensNameCategoryYearMonth(string category, string fullName, int year, string month);


        // Полный отчёт
        Task<IEnumerable<TemporaryData_ReportTimeDTO>> ExpensFull();
        // Отчёт за все года определенного user
        Task<IEnumerable<TemporaryData_ReportTimeDTO>> ExpensNameFullYear(string fullName);


        // Отчёт определенной категории за все года
        Task<IEnumerable<TemporaryData_ReportCategotyDTO>> ExpensCategoryFullYaer(string category);
        // Отчёт определенной категории за конкретный год
        Task<IEnumerable<TemporaryData_ReportCategotyDTO>> ExpensCategoryYaer(string category, int year);
    }
}
