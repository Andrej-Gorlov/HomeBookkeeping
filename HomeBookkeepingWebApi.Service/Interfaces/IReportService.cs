using HomeBookkeepingWebApi.Domain.Entity.TemporaryData;
using HomeBookkeepingWebApi.Domain.Paging;
using HomeBookkeepingWebApi.Domain.Response;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface IReportService
    {
        Task<IBaseResponse<List<string>>> GetAllCategoryServiceAsync();
        Task<IBaseResponse<List<string>>> GetAllFullNameUserServiceAsync();

        // Отчёт определенного user за конкретный год
        Task<IBaseResponse<PagedList<Report>>> ReportByNameUserYearServiceAsync(PagingQueryParameters paging, string fullName, int year);
        // Отчёт определенного user за конкретный год и месяц
        Task<IBaseResponse<Report>> ReportByNameUserYearMonthServiceAsync(string fullName, int year, string month);
        // Отчёт категории определенного user за конкретный год
        Task<IBaseResponse<PagedList<ReportCategory>>> ReportByCategoryNameUserYearServiceAsync(PagingQueryParameters paging, string category, string fullName, int year);
        //Отчёт категории определенного user за конкретный год и месяц
        Task<IBaseResponse<ReportCategory>> ReportByCategoryNameUserYearMonthServiceAsync(string category, string fullName, int year, string month);
        // Полный отчёт
        Task<IBaseResponse<PagedList<Report>>> FullReportServiceAsync(PagingQueryParameters paging);
        // Отчёт за все года определенного user
        Task<IBaseResponse<PagedList<Report>>> ReportAllYearsNameUserServiceAsync(PagingQueryParameters paging, string fullName);
        // Отчёт определенной категории за все года
        Task<IBaseResponse<PagedList<ReportCategory>>> ReportByCategoryAllYearsServiceAsync(PagingQueryParameters paging, string category);
        // Отчёт определенной категории за конкретный год
        Task<IBaseResponse<PagedList<ReportCategory>>> ReportByCategoryYaerServiceAsync(PagingQueryParameters paging, string category, int year);
    }
}
