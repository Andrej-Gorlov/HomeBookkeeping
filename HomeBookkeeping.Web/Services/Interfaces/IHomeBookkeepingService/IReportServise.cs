namespace HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService
{
    public interface IReportServise : IBaseService
    {
        Task<T> GetAllCategory<T>();
        Task<T> GetAllFullNameUser<T>();
        Task<T> ReportByNameUserAsync<T>(string fullName, int year);
        Task<T> ReportByNameUserYearMonthAsync<T>(string fullName, int year, string month);
        Task<T> ReportByCategoryNameUserYearAsync<T>(string category, string fullName, int year);
        Task<T> ReportByCategoryNameUserYearMonthAsync<T>(string category, string fullName, int year, string month);
        Task<T> FullReportAsync<T>();
        Task<T> ReportAllYearsNameUserAsync<T>(string fullName);
        Task<T> ReportByCategoryAllYearsAsync<T>(string category);
        Task<T> ReportByCategoryYaerAsync<T>(string category, int year);
    }
}
