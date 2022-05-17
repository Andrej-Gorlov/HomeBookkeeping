using HomeBookkeepingWebApi.Domain.Entity.TemporaryData;
using HomeBookkeepingWebApi.Domain.Response;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface IReportService
    {
        Task<IBaseResponse<List<string>>> GetAllCategoryServiceAsync();
        Task<IBaseResponse<List<string>>> GetAllFullNameUserServiceAsync();

        // Отчёт определенного user за конкретный год
        Task<IBaseResponse<IEnumerable<Report>>> ReportByNameUserYearServiceAsync(string fullName, int year);
        // Отчёт определенного user за конкретный год и месяц
        Task<IBaseResponse<Report>> ReportByNameUserYearMonthServiceAsync(string fullName, int year, string month);
        // Отчёт категории определенного user за конкретный год
        Task<IBaseResponse<IEnumerable<ReportCategory>>> ReportByCategoryNameUserYearServiceAsync(string category, string fullName, int year);
        //Отчёт категории определенного user за конкретный год и месяц
        Task<IBaseResponse<ReportCategory>> ReportByCategoryNameUserYearMonthServiceAsync(string category, string fullName, int year, string month);
        // Полный отчёт
        Task<IBaseResponse<IEnumerable<Report>>> FullReportServiceAsync();
        // Отчёт за все года определенного user
        Task<IBaseResponse<IEnumerable<Report>>> ReportAllYearsNameUserServiceAsync(string fullName);
        // Отчёт определенной категории за все года
        Task<IBaseResponse<IEnumerable<ReportCategory>>> ReportByCategoryAllYearsServiceAsync(string category);
        // Отчёт определенной категории за конкретный год
        Task<IBaseResponse<IEnumerable<ReportCategory>>> ReportByCategoryYaerServiceAsync(string category, int year);
    }
}
