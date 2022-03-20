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
        Task<IEnumerable<Report>> ReportByNameUserYear(string fullName, int year);
        // Отчёт определенного user за конкретный год и месяц 
        Task<Report> ReportByNameUserYearMonth(string fullName, int year, string month);


        // Отчёт категории определенного user за конкретный год
        Task<IEnumerable<ReportCategory>> ReportByCategoryNameUserYear(string category, string fullName, int year);
        //Отчёт категории определенного user за конкретный год и месяц 
        Task<ReportCategory> ReportByCategoryNameUserYearMonth(string category, string fullName, int year, string month);


        // Полный отчёт
        Task<IEnumerable<Report>> FullReport();
        // Отчёт за все года определенного user
        Task<IEnumerable<Report>> ReportAllYearsNameUser(string fullName);


        // Отчёт определенной категории за все года 
        Task<IEnumerable<ReportCategory>> ReportByCategoryAllYears(string category);
        // Отчёт определенной категории за конкретный год
        Task<IEnumerable<ReportCategory>> ReportByCategoryYaer(string category, int year);
    }
}
