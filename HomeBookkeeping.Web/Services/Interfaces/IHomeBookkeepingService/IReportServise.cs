namespace HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService
{
    public interface IReportServise : IBaseService
    {
        Task<T> GetAllCategory<T>();
        Task<T> GetAllFullNameUser<T>();
        Task<T> ExpensNameYearReportAsync<T>(string fullName, int year);
        Task<T> ExpensNameYearMonthReportAsync<T>(string fullName, int year, string month);
        Task<T> ExpensNameCategoryYearReportAsync<T>(string category, string fullName, int year);
        Task<T> ExpensNameCategoryYearMonthReportAsync<T>(string category, string fullName, int year, string month);
        Task<T> ExpensFullReportAsync<T>();
        Task<T> ExpensNameFullYearReportAsync<T>(string fullName);
        Task<T> ExpensCategoryFullYaerReportAsync<T>(string category);
        Task<T> ExpensCategoryYaerReportAsync<T>(string category, int year);
    }
}
