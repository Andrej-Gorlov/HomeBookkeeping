using HomeBookkeepingWebApi.Domain.Entity.TemporaryData;
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
        Task<List<string>> GetAllFullNameUser();
        // Отчёт определенного user за конкретный год
        Task<IEnumerable<TemporaryDataReportTime>> ExpensNameYear(string fullName, int year);
        // Отчёт определенного user за конкретный год и месяц 
        Task<TemporaryDataReportTime> ExpensNameYearMonth(string fullName, int year, string month);


        // Отчёт категории определенного user за конкретный год
        Task<IEnumerable<TemporaryDataReportCategoty>> ExpensNameCategoryYear(string category, string fullName, int year);
        //Отчёт категории определенного user за конкретный год и месяц 
        Task<TemporaryDataReportCategoty> ExpensNameCategoryYearMonth(string category, string fullName, int year, string month);


        // Полный отчёт
        Task<IEnumerable<TemporaryDataReportTime>> ExpensFull();
        // Отчёт за все года определенного user
        Task<IEnumerable<TemporaryDataReportTime>> ExpensNameFullYear(string fullName);


        // Отчёт определенной категории за все года
        Task<IEnumerable<TemporaryDataReportCategoty>> ExpensCategoryFullYaer(string category);
        // Отчёт определенной категории за конкретный год
        Task<IEnumerable<TemporaryDataReportCategoty>> ExpensCategoryYaer(string category, int year);
    }
}
