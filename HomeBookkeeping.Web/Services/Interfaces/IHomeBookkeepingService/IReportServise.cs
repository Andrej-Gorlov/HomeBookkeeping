using HomeBookkeeping.Web.Models.Paging;

namespace HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService
{
    public interface IReportServise : IBaseService
    {
        Task<T> GetAllCategory<T>();
        Task<T> GetAllFullNameUser<T>();
        Task<T> ReportByNameUserAsync<T>(PagingParameters parameters, string fullName, int year);
        Task<T> ReportByNameUserYearMonthAsync<T>(string fullName, int year, string month);
        Task<T> ReportByCategoryNameUserYearAsync<T>(PagingParameters parameters, string category, string fullName, int year);
        Task<T> ReportByCategoryNameUserYearMonthAsync<T>(string category, string fullName, int year, string month);
        Task<T> FullReportAsync<T>(PagingParameters parameters);
        Task<T> ReportAllYearsNameUserAsync<T>(PagingParameters parameters, string fullName);
        Task<T> ReportByCategoryAllYearsAsync<T>(PagingParameters parameters, string category);
        Task<T> ReportByCategoryYaerAsync<T>(PagingParameters parameters, string category, int year);
    }
}
