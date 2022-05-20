using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity.TemporaryData;
using HomeBookkeepingWebApi.Domain.Paging;
using HomeBookkeepingWebApi.Domain.Response;
using HomeBookkeepingWebApi.Service.Helpers;
using HomeBookkeepingWebApi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeBookkeepingWebApi.Service.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IUserRepository _userRep;
        private readonly ITransactionRepository _transactionRep;
        public ReportService(IUserRepository userRep, ITransactionRepository transactionRep)
        {
            _userRep = userRep;
            _transactionRep = transactionRep;
        }
        public async Task<IBaseResponse<PagedList<ReportCategory>>> ReportByCategoryAllYearsServiceAsync(PagingQueryParameters paging, string category)
        {
            var users = await _userRep.GetAsync();
            var nameUsers = users.Select(x => x.FullName).Distinct().ToList();

            var transactions = await _transactionRep.GetAsync();
            var listYear = transactions.Select(x => x.DateOperations.Year).Distinct().ToList();
            var nameCategory = transactions
                .FirstOrDefault(x => x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""));

            List<ReportCategory> reportCategoryAllYears = new();
            foreach (var itemUser in nameUsers)
            {
                foreach (var itemYear in listYear)
                {
                    var listMonth = transactions
                        .Where(x => x.DateOperations.Year == itemYear)
                        .Select(x => x.DateOperations.Month).Distinct().ToList();
                    foreach (var itemMonth in listMonth)
                    {
                        DateTime dateTime = ReportHelper.DefinitionDateTime(itemYear, itemMonth.ToString());
                        ReportCategory reportMonth = new()
                        {
                            FullName = itemUser,
                            DateTime = dateTime,
                            Category = nameCategory.Category,
                            recipientsData = ReportHelper.ListRecipient(transactions.ToList(), dateTime, nameCategory.Category, itemUser, true),
                            Sum = transactions.Where(x => x.UserFullName == itemUser &&
                                                     x.DateOperations.Year == dateTime.Year &&
                                                     x.DateOperations.Month == dateTime.Month &&
                                                     x.Category == nameCategory.Category).Sum(x => x.Sum)
                        };
                        if (reportMonth.Sum != 0)
                        {
                            reportCategoryAllYears.Add(reportMonth);
                        }
                    }
                }
            }
            var baseResponse = new BaseResponse<PagedList<ReportCategory>>();
            if (reportCategoryAllYears is null)
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за все года пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за все года.";

            baseResponse.Result = PagedList<ReportCategory>.ToPagedList(reportCategoryAllYears, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<ReportCategory>>> ReportByCategoryYaerServiceAsync(PagingQueryParameters paging, string category, int year)
        {
            var transactions = await _transactionRep.GetAsync();
            var nameCategory = transactions
                .FirstOrDefault(x => x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", "")); ;

            var nameUsers = _userRep.GetAsync().Result.AsQueryable().Select(x => x.FullName).Distinct().ToList();

            List<ReportCategory> reportCategoryAllYears = new();
            foreach (var itemUser in nameUsers)
            {
                List<int> listMonth = transactions
                    .Where(x => x.DateOperations.Year == year)
                    .Select(x => x.DateOperations.Month).Distinct().ToList();
                foreach (var itemMonth in listMonth)
                {
                    DateTime dateTime = ReportHelper.DefinitionDateTime(year, itemMonth.ToString());
                    ReportCategory reportMonth = new()
                    {
                        FullName = itemUser,
                        DateTime = dateTime,
                        Category = nameCategory.Category,
                        recipientsData = ReportHelper.ListRecipient(transactions.ToList(), dateTime, nameCategory.Category, itemUser, true),
                        Sum = transactions.Where(x => x.UserFullName == itemUser &&
                                                 x.DateOperations.Year == dateTime.Year &&
                                                 x.DateOperations.Month == dateTime.Month &&
                                                 x.Category == nameCategory.Category).Sum(x => x.Sum)
                    };
                    if (reportMonth.Sum != 0)
                    {
                        reportCategoryAllYears.Add(reportMonth);
                    }
                }
            }
            var baseResponse = new BaseResponse<PagedList<ReportCategory>>();

            if (reportCategoryAllYears is null)
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за год [{year}].";

            baseResponse.Result = PagedList<ReportCategory>.ToPagedList(reportCategoryAllYears, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<Report>>> FullReportServiceAsync(PagingQueryParameters paging)
        {
            var users = await _userRep.GetAsync();
            var nameUsers = users.Select(x => x.FullName).Distinct().ToList();

            var transactions = await _transactionRep.GetAsync();
            var listYear = transactions.Select(x => x.DateOperations.Year).Distinct().ToList();

            List<Report> fullReport = new();
            foreach (var itemName in nameUsers)
            {
                foreach (var itemYear in listYear)
                {
                    var listMonth = transactions
                        .Where(x => x.DateOperations.Year == itemYear)
                        .Select(x => x.DateOperations.Month).Distinct().ToList();
                    foreach (var itemMonth in listMonth)
                    {
                        DateTime dateTime = ReportHelper.DefinitionDateTime(itemYear, itemMonth.ToString());
                        Report reportMonth = new()
                        {
                            FullName = itemName,
                            DateTime = dateTime,
                            Сategories = ReportHelper.ListTypeExpense(transactions.ToList(), dateTime, itemName, true),
                            Sum = transactions.Where(x => x.DateOperations.Year == dateTime.Year
                                                    && x.DateOperations.Month == dateTime.Month
                                                    && x.UserFullName == itemName).Sum(x => x.Sum)
                        };
                        if (reportMonth.Sum != 0)
                        {
                            fullReport.Add(reportMonth);
                        }
                    }
                }
            }
            var baseResponse = new BaseResponse<PagedList<Report>>();
            if (fullReport is null)
                baseResponse.DisplayMessage = $"Список всех расходов пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов.";

            baseResponse.Result = PagedList<Report>.ToPagedList(fullReport, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<ReportCategory>>> ReportByCategoryNameUserYearServiceAsync(PagingQueryParameters paging, string category, string fullName, int year)
        {
            var users = await _userRep.GetAsync();
            var name = users.FirstOrDefault(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));

            var transactions = await _transactionRep.GetAsync();
            var listMonth = transactions.Where(x => x.DateOperations.Year == year)
                .Select(x => x.DateOperations.Month).Distinct().ToList();

            List<ReportCategory> reportCategoryNameUserYear = new();
            foreach (var item in listMonth)
            {
                DateTime dateTime = ReportHelper.DefinitionDateTime(year, item.ToString());
                ReportCategory reportMonth = new()
                {
                    FullName = name.FullName,
                    DateTime = dateTime,
                    Category = category,
                    recipientsData = ReportHelper.ListRecipient(transactions.ToList(), dateTime, category, name.FullName),
                    Sum = transactions.Where(
                            x => x.UserFullName == name.FullName &&
                            x.DateOperations.Year == dateTime.Year &&
                            x.DateOperations.Month == dateTime.Month &&
                            x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""))
                            .Sum(x => x.Sum)
                };
                if (reportMonth.Sum != 0)
                {
                    reportCategoryNameUserYear.Add(reportMonth);
                }
            }
            var baseResponse = new BaseResponse<PagedList<ReportCategory>>();
            if (reportCategoryNameUserYear is null)
                baseResponse.DisplayMessage = $"Список расходов по категории [{category}] пользователя [{fullName}] за год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов по категории [{category}] пользователя [{fullName}] за год [{year}].";

            baseResponse.Result = PagedList<ReportCategory>.ToPagedList(reportCategoryNameUserYear, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<ReportCategory>> ReportByCategoryNameUserYearMonthServiceAsync(string category, string fullName, int year, string month)
        {
            DateTime dateTime = ReportHelper.DefinitionDateTime(year, month);
            var users = await _userRep.GetAsync();
            var name = users.FirstOrDefault(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));

            var transactions = await _transactionRep.GetAsync();

            ReportCategory reportCategoryNameUserYearMonth = new()
            {
                FullName = name.FullName,
                DateTime = dateTime,
                Category = category,
                recipientsData = ReportHelper.ListRecipient(transactions.ToList(), dateTime, category, name.FullName, true),
                Sum = transactions.Where(
                                    x => x.UserFullName == name.FullName &&
                                    x.DateOperations.Year == dateTime.Year &&
                                    x.DateOperations.Month == dateTime.Month &&
                                    x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""))
                                    .Sum(x => x.Sum)
            };
            var baseResponse = new BaseResponse<ReportCategory>();

            if (reportCategoryNameUserYearMonth.Sum == 0)
                baseResponse.DisplayMessage = $"Пользователь [{fullName}] за [{month}]/[{year}] г. по категории [{category}] не совершал расходов.";
            if (reportCategoryNameUserYearMonth is null)
                baseResponse.DisplayMessage = $"Отчёт по категории [{category}] пользователя [{fullName}] за год [{year}] и месеца [{month}] не найден.";
            else
                baseResponse.DisplayMessage = $"Отчёт по категории [{category}] пользователя [{fullName}] за год [{year}] и месеца [{month}]";

            baseResponse.Result = reportCategoryNameUserYearMonth;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<Report>>> ReportAllYearsNameUserServiceAsync(PagingQueryParameters paging, string fullName)
        {
            var users = await _userRep.GetAsync();
            var name = users.FirstOrDefault(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));

            var transactions = await _transactionRep.GetAsync();
            var listYear = transactions.Select(x => x.DateOperations.Year).Distinct().ToList();

            List<Report> reportAllYearsNameUser = new();
            foreach (var itemYear in listYear)
            {
                var listMonth = transactions
                    .Where(x => x.DateOperations.Year == itemYear)
                    .Select(x => x.DateOperations.Month).Distinct().ToList();
                foreach (var itemMonth in listMonth)
                {
                    DateTime dateTime = ReportHelper.DefinitionDateTime(itemYear, itemMonth.ToString());
                    Report reportMonth = new()
                    {
                        FullName = name.FullName,
                        DateTime = dateTime,
                        Сategories = ReportHelper.ListTypeExpense(transactions.ToList(), dateTime, name.FullName, true),
                        Sum = transactions.Where(x => x.DateOperations.Year == dateTime.Year
                                                 && x.DateOperations.Month == dateTime.Month
                                                 && x.UserFullName == name.FullName).Sum(x => x.Sum)
                    };
                    if (reportMonth.Sum != 0)
                    {
                        reportAllYearsNameUser.Add(reportMonth);
                    }
                }
            }
            var baseResponse = new BaseResponse<PagedList<Report>>();
            if (reportAllYearsNameUser is null)
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все года пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все года.";

            baseResponse.Result = PagedList<Report>.ToPagedList(reportAllYearsNameUser, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<Report>>> ReportByNameUserYearServiceAsync(PagingQueryParameters paging, string fullName, int year)
        {
            var users = await _userRep.GetAsync();
            var name = users.FirstOrDefault(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));

            var transactions = await _transactionRep.GetAsync();
            var listMonth = transactions
                .Where(x => x.DateOperations.Year == year)
                .Select(x => x.DateOperations.Month).Distinct().ToList();

            List<Report> reportByNameUserYear = new();
            foreach (var item in listMonth)
            {
                DateTime dateTime = ReportHelper.DefinitionDateTime(year, item.ToString());
                Report reportMonth = new()
                {
                    FullName = name.FullName,
                    DateTime = dateTime,
                    Сategories = ReportHelper.ListTypeExpense(transactions.ToList(), dateTime, name.FullName, true),
                    Sum = transactions.Where(x => x.DateOperations.Year == dateTime.Year &&
                                             x.DateOperations.Month == dateTime.Month &&
                                             x.UserFullName == name.FullName).Sum(x => x.Sum)
                };
                if (reportMonth.Sum != 0)
                {
                    reportByNameUserYear.Add(reportMonth);
                }
            }
            var baseResponse = new BaseResponse<PagedList<Report>>();
            if (reportByNameUserYear is null)
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все год [{year}].";

            baseResponse.Result = PagedList<Report>.ToPagedList(reportByNameUserYear, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<Report>> ReportByNameUserYearMonthServiceAsync(string fullName, int year, string month)
        {
            DateTime dateTime = ReportHelper.DefinitionDateTime(year, month);
            var users = await _userRep.GetAsync();
            var name = users.FirstOrDefault(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));

            var transactions = await _transactionRep.GetAsync();

            Report reportByNameUserYearMonth = new()
            {
                FullName = name.FullName,
                DateTime = dateTime,
                Сategories = ReportHelper.ListTypeExpense(transactions.ToList(), dateTime, name.FullName, true),
                Sum = transactions.Where(x => x.DateOperations.Year == dateTime.Year &&
                                         x.DateOperations.Month == dateTime.Month &&
                                         x.UserFullName == name.FullName).Sum(x => x.Sum)
            };
            var baseResponse = new BaseResponse<Report>();

            if (reportByNameUserYearMonth.Sum == 0)
                baseResponse.DisplayMessage = $"Пользователь [{fullName}] за [{month}]/[{year}] г. не совершал расходов.";
            if (reportByNameUserYearMonth is null)
                baseResponse.DisplayMessage = $"Отчёт пользователя [{fullName}] за год [{year}] и месеца [{month}] не найден.";
            else
                baseResponse.DisplayMessage = $"Отчёт пользователя [{fullName}] за год [{year}] и месеца [{month}]";

            baseResponse.Result = reportByNameUserYearMonth;
            return baseResponse;
        }
        public async Task<IBaseResponse<List<string>>> GetAllCategoryServiceAsync()
        {
            var listCategory = await _transactionRep.GetAsync().Result.AsQueryable()
                .Select(x => x.Category).Distinct().ToArrayAsync();

            var baseResponse = new BaseResponse<List<string>>();

            if (listCategory.Count() > 0)
                baseResponse.DisplayMessage = $"Список категорий.";
            else
                baseResponse.DisplayMessage = $"Список категорий пуст.";

            baseResponse.Result = listCategory.ToList();
            return baseResponse;
        }
        public async Task<IBaseResponse<List<string>>> GetAllFullNameUserServiceAsync()
        {
            var listFullName = await _transactionRep.GetAsync().Result.AsQueryable()
                .Select(x => x.UserFullName).Distinct().ToArrayAsync();

            var baseResponse = new BaseResponse<List<string>>();
            if (listFullName.Count() > 0)
                baseResponse.DisplayMessage = $"Список пользователей.";
            else
                baseResponse.DisplayMessage = $"Список пользователей пуст.";
            baseResponse.Result = listFullName.ToList();
            return baseResponse;
        }
    }
}
