using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity.TemporaryData;
using HomeBookkeepingWebApi.Domain.Response;
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
        public async Task<IBaseResponse<IEnumerable<ReportCategory>>> ReportByCategoryAllYearsServiceAsync(string category)
        {
            var nameUsers = _userRep.GetAsync().Result.AsQueryable().Select(x => x.FullName).Distinct().ToList();

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
                        DateTime dateTime = DefinitionDateTime(itemYear, itemMonth.ToString());
                        ReportCategory reportMonth = new()
                        {
                            FullName = itemUser,
                            DateTime = dateTime,
                            Category = nameCategory.Category,
                            recipientsData = ListRecipient(transactions.ToList(), dateTime, nameCategory.Category, itemUser, true),
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
            var baseResponse = new BaseResponse<IEnumerable<ReportCategory>>();
            if (reportCategoryAllYears is null)
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за все года пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за все года.";
            
            baseResponse.Result = reportCategoryAllYears;
            return baseResponse;
        }
        public async Task<IBaseResponse<IEnumerable<ReportCategory>>> ReportByCategoryYaerServiceAsync(string category, int year)
        {
            var transactions = await _transactionRep.GetAsync();
            var nameCategory = transactions
                .FirstOrDefault(x => x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""));;
           
            var nameUsers = _userRep.GetAsync().Result.AsQueryable().Select(x => x.FullName).Distinct().ToList();
            
            List<ReportCategory> reportCategoryAllYears = new();
            foreach (var itemUser in nameUsers)
            {
                List<int> listMonth = transactions
                    .Where(x => x.DateOperations.Year == year)
                    .Select(x => x.DateOperations.Month).Distinct().ToList();
                foreach (var itemMonth in listMonth)
                {
                    DateTime dateTime = DefinitionDateTime(year, itemMonth.ToString());
                    ReportCategory reportMonth = new()
                    {
                        FullName = itemUser,
                        DateTime = dateTime,
                        Category = nameCategory.Category,
                        recipientsData = ListRecipient(transactions.ToList(),dateTime, nameCategory.Category, itemUser, true),
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
            var baseResponse = new BaseResponse<IEnumerable<ReportCategory>>();
            
            if (reportCategoryAllYears is null)
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов по категории [{category}] за год [{year}].";
            
            baseResponse.Result = reportCategoryAllYears;
            return baseResponse;
        }
        public async Task<IBaseResponse<IEnumerable<Report>>> FullReportServiceAsync()
        {
            var nameUsers = _userRep.GetAsync().Result.AsQueryable().Select(x => x.FullName).Distinct().ToList();

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
                        DateTime dateTime = DefinitionDateTime(itemYear, itemMonth.ToString());
                        Report reportMonth = new()
                        {
                            FullName = itemName,
                            DateTime = dateTime,
                            Сategories = ListTypeExpense(transactions.ToList(), dateTime, itemName, true),
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
            var baseResponse = new BaseResponse<IEnumerable<Report>>();
            if (fullReport is null)
                baseResponse.DisplayMessage = $"Список всех расходов пуст.";
            else
                baseResponse.DisplayMessage = $"Список всех расходов.";
           
            baseResponse.Result = fullReport;
            return baseResponse;
        }
        public async Task<IBaseResponse<IEnumerable<ReportCategory>>> ReportByCategoryNameUserYearServiceAsync(string category, string fullName, int year)
        {
            var name = await _userRep.GetAsync().Result.AsQueryable()
                .FirstOrDefaultAsync(
                x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));

            var transactions = await _transactionRep.GetAsync();
            var listMonth = transactions.Where(x => x.DateOperations.Year == year)
                .Select(x => x.DateOperations.Month).Distinct().ToList();
           
            List<ReportCategory> reportCategoryNameUserYear = new();
            foreach (var item in listMonth)
            {
                DateTime dateTime = DefinitionDateTime(year, item.ToString());
                ReportCategory reportMonth = new()
                {
                    FullName = name.FullName,
                    DateTime = dateTime,
                    Category = category,
                    recipientsData = ListRecipient(transactions.ToList(), dateTime, category, name.FullName),
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
            var baseResponse = new BaseResponse<IEnumerable<ReportCategory>>();
            if (reportCategoryNameUserYear is null)
                baseResponse.DisplayMessage = $"Список расходов по категории [{category}] пользователя [{fullName}] за год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов по категории [{category}] пользователя [{fullName}] за год [{year}].";
           
            baseResponse.Result = reportCategoryNameUserYear;
            return baseResponse;
        }
        public async Task<IBaseResponse<ReportCategory>> ReportByCategoryNameUserYearMonthServiceAsync(string category, string fullName, int year, string month)
        {
            DateTime dateTime = DefinitionDateTime(year, month);

            var name = await _userRep.GetAsync().Result.AsQueryable().FirstOrDefaultAsync(
                x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));

            var transactions = await _transactionRep.GetAsync();

            ReportCategory reportCategoryNameUserYearMonth = new()
            {
                FullName = name.FullName,
                DateTime = dateTime,
                Category = category,
                recipientsData = ListRecipient(transactions.ToList(), dateTime, category, name.FullName, true),
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
        public async Task<IBaseResponse<IEnumerable<Report>>> ReportAllYearsNameUserServiceAsync(string fullName)
        {
            var name = await _userRep.GetAsync().Result.AsQueryable().FirstOrDefaultAsync(
                x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));

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
                    DateTime dateTime = DefinitionDateTime(itemYear, itemMonth.ToString());
                    Report reportMonth = new()
                    {
                        FullName = name.FullName,
                        DateTime = dateTime,
                        Сategories = ListTypeExpense(transactions.ToList(), dateTime, name.FullName, true),
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
            var baseResponse = new BaseResponse<IEnumerable<Report>>();
            if (reportAllYearsNameUser is null)
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все года пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все года.";
            
            baseResponse.Result = reportAllYearsNameUser;
            return baseResponse;
        }
        public async Task<IBaseResponse<IEnumerable<Report>>> ReportByNameUserYearServiceAsync(string fullName, int year)
        {
            var name = await _userRep.GetAsync().Result.AsQueryable().FirstOrDefaultAsync(
                x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));

            var transactions = await _transactionRep.GetAsync();
            var listMonth = transactions
                .Where(x => x.DateOperations.Year == year)
                .Select(x => x.DateOperations.Month).Distinct().ToList();
           
            List<Report> reportByNameUserYear = new();
            foreach (var item in listMonth)
            {
                DateTime dateTime = DefinitionDateTime(year, item.ToString());
                Report reportMonth = new()
                {
                    FullName = name.FullName,
                    DateTime = dateTime,
                    Сategories = ListTypeExpense(transactions.ToList(), dateTime, name.FullName, true),
                    Sum = transactions.Where(x => x.DateOperations.Year == dateTime.Year &&
                                             x.DateOperations.Month == dateTime.Month &&
                                             x.UserFullName == name.FullName).Sum(x => x.Sum)
                };
                if (reportMonth.Sum != 0)
                {
                    reportByNameUserYear.Add(reportMonth);
                }
            }
            var baseResponse = new BaseResponse<IEnumerable<Report>>();
            if (reportByNameUserYear is null)
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все год [{year}] пуст.";
            else
                baseResponse.DisplayMessage = $"Список расходов пользователя [{fullName}] за все год [{year}].";
            
            baseResponse.Result = reportByNameUserYear;
            return baseResponse;
        }
        public async Task<IBaseResponse<Report>> ReportByNameUserYearMonthServiceAsync(string fullName, int year, string month)
        {
            DateTime dateTime = DefinitionDateTime(year, month);
            var name = await _userRep.GetAsync().Result.AsQueryable()
                .FirstOrDefaultAsync(
                x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));

            var transactions = await _transactionRep.GetAsync();

            Report reportByNameUserYearMonth = new()
            {
                FullName = name.FullName,
                DateTime = dateTime,
                Сategories = ListTypeExpense(transactions.ToList(), dateTime, name.FullName, true),
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
        //--------------------------- Private method ---------------------------\\
        private DateTime DefinitionDateTime(int year, string month = "1")
        {
            var m = month.ToUpper().Replace(" ", "");

            if (m == "ЯНВАРЬ" || m == "JANUARY" || m == "1" || m == "01")
                return new DateTime(year, 1, 1);
            else if (m == "ФЕВРАЛЬ" || m == "FEBRUARY" || m == "2" || m == "02")
                return new DateTime(year, 2, 1);
            else if (m == "МАРТ" || m == "MARCH" || m == "3" || m == "03")
                return new DateTime(year, 3, 1);
            else if (m == "АПРЕЛЬ" || m == "APRIL" || m == "4" || m == "04")
                return new DateTime(year, 4, 1);
            else if (m == "МАЙ" || m == "MAY" || m == "5" || m == "05")
                return new DateTime(year, 5, 1);
            else if (m == "ИЮНЬ" || m == "JUNE" || m == "6" || m == "06")
                return new DateTime(year, 6, 1);
            else if (m == "ИЮЛЬ" || m == "JULY" || m == "7" || m == "07")
                return new DateTime(year, 7, 1);
            else if (m == "АВГУСТ" || m == "AUGUST" || m == "8" || m == "08")
                return new DateTime(year, 8, 1);
            else if (m == "СЕНТЯБРЬ" || m == "SEPTEMBER" || m == "9" || m == "09")
                return new DateTime(year, 9, 1);
            else if (m == "ОКТЯБРЬ" || m == "OCTOBER" || m == "10")
                return new DateTime(year, 10, 1);
            else if (m == "НОЯБРЬ" || m == "NOVEMBER" || m == "11")
                return new DateTime(year, 11, 1);
            else if (m == "ДЕКАБРЬ" || m == "DECEMBER" || m == "12")
                return new DateTime(year, 12, 1);
            else return new DateTime(0);
        }
        private List<TypeExpenseAndSum> ListTypeExpense(List<TransactionDTO> transactions, DateTime dateTime, string fullName, bool Month = false)
        {
            List<TypeExpenseAndSum> listTypeExpense = new List<TypeExpenseAndSum>();
            var listCategory = transactions.Select(x => x.Category).Distinct().ToList();

            foreach (string category in listCategory)
            {
                var typeEAS = new TypeExpenseAndSum();
                typeEAS.NameTypeExpense = category;
                if (Month)
                {
                    typeEAS.Recipients = ListRecipient(transactions, dateTime, category, fullName, true);

                    typeEAS.SumTypeExpense = transactions
                        .Where(x => x.DateOperations.Year == dateTime.Year 
                        && x.DateOperations.Month == dateTime.Month 
                        && x.Category == category 
                        && x.UserFullName == fullName).Sum(x => x.Sum);
                }
                else
                {
                    typeEAS.Recipients = ListRecipient(transactions, dateTime, category, fullName);

                    typeEAS.SumTypeExpense = transactions
                        .Where(x => x.DateOperations.Year == dateTime.Year 
                        && x.Category == category 
                        && x.UserFullName == fullName).Sum(x => x.Sum);
                }
                if (typeEAS.SumTypeExpense != 0)
                {
                    listTypeExpense.Add(typeEAS);
                }
            }
            return listTypeExpense;
        }
        private List<ReportRecipient> ListRecipient(List<TransactionDTO> transactions,DateTime dateTime, string category, string fullName, bool Month = false)
        {
            List<ReportRecipient> listRecipientData = new();

            var listRecipientName = transactions.Select(x => x.RecipientName).Distinct().ToList();

            foreach (var item in listRecipientName)
            {
                ReportRecipient recipient = new();
                recipient.NameRecipient = item;
                if (Month)
                {
                    recipient.NameRecipientSum = transactions
                        .Where(x => x.DateOperations.Year == dateTime.Year
                        && x.DateOperations.Month == dateTime.Month
                        && x.Category == category
                        && x.RecipientName == item
                        && x.UserFullName == fullName).Sum(x => x.Sum);
                }
                else
                {
                    recipient.NameRecipientSum = transactions
                        .Where(x => x.DateOperations.Year == dateTime.Year 
                        && x.Category == category && x.RecipientName == item 
                        && x.UserFullName == fullName).Sum(x => x.Sum);
                } 
                if (recipient.NameRecipientSum != 0)
                {
                    listRecipientData.Add(recipient);
                }    
            }
            return listRecipientData;
        }
    }
}
