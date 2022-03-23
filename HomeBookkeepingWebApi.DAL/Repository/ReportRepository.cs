using AutoMapper;
using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.DAL.Repository.Static;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity.TemporaryData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.DAL.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public ReportRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ReportCategory>> ReportByCategoryAllYears(string category)
        {
            var listUser = await _db.User.Select(x => x.FullName).Distinct().ToListAsync();
            List<int> listYear = await _db.Transaction.Select(x => x.DateOperations.Year).Distinct().ToListAsync();
            var nameCategory = _db.Transaction.FirstOrDefault(x => x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""));
            List<ReportCategory> listTemporaryDRC = new();
            foreach (var itemUser in listUser)
            {
                foreach (var itemYear in listYear)
                {
                    List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == itemYear).Select(x => x.DateOperations.Month).Distinct().ToList();
                    foreach (var itemMonth in listMonth)
                    {
                        DateTime dateTime = DefinePoly.DefinitionDateTime(itemYear, itemMonth.ToString());
                        ReportCategory temporaryDRC = new()
                        {
                            FullName = itemUser,
                            DateTime = dateTime,
                            Category = nameCategory.Category,
                            recipientsData = ListRecipient(dateTime, nameCategory.Category, itemUser, true),
                            Sum = (decimal)_db.Transaction.Where(x => x.UserFullName == itemUser &&
                                                           x.DateOperations.Year == dateTime.Year &&
                                                           x.DateOperations.Month == dateTime.Month &&
                                                           x.Category == nameCategory.Category).Sum(x => x.Sum)
                        };
                        if(temporaryDRC.Sum!=0) listTemporaryDRC.Add(temporaryDRC);
                    }
                }
            }
            return listTemporaryDRC;
        }

        public async Task<IEnumerable<ReportCategory>> ReportByCategoryYaer(string category, int year)
        {
            var nameCategory = _db.Transaction.FirstOrDefault(x => x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""));
            var listUser = await _db.User.Select(x => x.FullName).Distinct().ToListAsync();
            List<ReportCategory> listTemporaryDRC = new();
            foreach (var itemUser in listUser)
            {
                List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == year).Select(x => x.DateOperations.Month).Distinct().ToList();
                foreach (var itemMonth in listMonth)
                {
                    DateTime dateTime = DefinePoly.DefinitionDateTime(year, itemMonth.ToString());
                    ReportCategory temporaryDRC = new()
                    {
                        FullName = itemUser,
                        DateTime = dateTime,
                        Category = nameCategory.Category,
                        recipientsData = ListRecipient(dateTime, nameCategory.Category, itemUser, true),
                        Sum = (decimal)_db.Transaction.Where(x => x.UserFullName == itemUser &&
                                                       x.DateOperations.Year == dateTime.Year &&
                                                       x.DateOperations.Month == dateTime.Month &&
                                                       x.Category == nameCategory.Category).Sum(x => x.Sum)
                    };
                    if(temporaryDRC.Sum!=0) listTemporaryDRC.Add(temporaryDRC);
                }
            }
            return listTemporaryDRC;
        }

        public async Task<IEnumerable<Report>> FullReport()
        {
            var listUser = await _db.User.Select(x => x.FullName).Distinct().ToListAsync();
            List<int> listYear = await _db.Transaction.Select(x => x.DateOperations.Year).Distinct().ToListAsync();
            List<Report> listTemporaryDRT = new();
            foreach (var itemName in listUser)
            {
                foreach (var itemYear in listYear)
                {
                    List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == itemYear).Select(x => x.DateOperations.Month).Distinct().ToList();
                    foreach (var itemMonth in listMonth)
                    {
                        DateTime dateTime = DefinePoly.DefinitionDateTime(itemYear, itemMonth.ToString());
                        Report temDRT = new()
                        {
                            FullName = itemName,
                            DateTime = dateTime,
                            Сategories = ListTypeExpense(dateTime, itemName, true),
                            Sum = (decimal)_db.Transaction.Where(
                                x => x.DateOperations.Year == dateTime.Year
                                && x.DateOperations.Month == dateTime.Month
                                && x.UserFullName == itemName).Sum(x => x.Sum)
                        };
                        if(temDRT.Sum!=0) listTemporaryDRT.Add(temDRT);
                    }
                }
            }
            return listTemporaryDRT;
        }

        public async Task<IEnumerable<ReportCategory>> ReportByCategoryNameUserYear(string category, string fullName, int year)
        {
            var name = await _db.User.FirstOrDefaultAsync(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));
            List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == year).Select(x => x.DateOperations.Month).Distinct().ToList();
            List<ReportCategory> listTRC = new();
            foreach (var item in listMonth)
            {
                DateTime dateTime = DefinePoly.DefinitionDateTime(year, item.ToString());
                ReportCategory tDRC = new()
                {
                    FullName = name.FullName,
                    DateTime = dateTime,
                    Category = category,
                    recipientsData = ListRecipient(dateTime, category, name.FullName),
                    Sum = (decimal)_db.Transaction.Where(
                        x => x.UserFullName == name.FullName &&
                        x.DateOperations.Year == dateTime.Year &&
                        x.DateOperations.Month == dateTime.Month &&
                        x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""))
                        .Sum(x => x.Sum)
                };
                if(tDRC.Sum!=0) listTRC.Add(tDRC);
            }
            return listTRC;
        }

        public async Task<ReportCategory> ReportByCategoryNameUserYearMonth(string category, string fullName, int year, string month)
        {
            DateTime dateTime = DefinePoly.DefinitionDateTime(year, month);
            var name = await _db.User.FirstOrDefaultAsync(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));
            ReportCategory tDRC = new()
            {
                FullName = name.FullName,
                DateTime = dateTime,
                Category = category,
                recipientsData = ListRecipient(dateTime, category, name.FullName, true),
                Sum = (decimal)_db.Transaction.Where(
                    x => x.UserFullName == name.FullName &&
                    x.DateOperations.Year == dateTime.Year &&
                    x.DateOperations.Month == dateTime.Month &&
                    x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""))
                    .Sum(x => x.Sum)
            };
            return tDRC;
        }

        public async Task<IEnumerable<Report>> ReportAllYearsNameUser(string fullName)
        {
            var name = await _db.User.FirstOrDefaultAsync(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));
            List<int> listYear = await _db.Transaction.Select(x => x.DateOperations.Year).Distinct().ToListAsync();
            List<Report> listTD_RT = new();
            foreach (var itemYear in listYear)
            {
                List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == itemYear).Select(x => x.DateOperations.Month).Distinct().ToList();
                foreach (var itemMonth in listMonth)
                {
                    DateTime dateTime = DefinePoly.DefinitionDateTime(itemYear, itemMonth.ToString());
                    Report temporaryDRT = new()
                    {
                        FullName = name.FullName,
                        DateTime = dateTime,
                        Сategories = ListTypeExpense(dateTime, name.FullName, true),
                        Sum = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year
                                                            && x.DateOperations.Month == dateTime.Month
                                                            && x.UserFullName == name.FullName).Sum(x => x.Sum)
                    };
                    if(temporaryDRT.Sum!=0) listTD_RT.Add(temporaryDRT);
                }
            }
            return listTD_RT;
        }

        public async Task<IEnumerable<Report>> ReportByNameUserYear(string fullName, int year)
        {
            var name = await _db.User.FirstOrDefaultAsync(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));
            List<Report> listTD_RT = new ();
            List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == year).Select(x => x.DateOperations.Month).Distinct().ToList();
            foreach (var item in listMonth)
            {
                DateTime dateTime = DefinePoly.DefinitionDateTime(year, item.ToString());
                Report temporaryDRT = new()
                {
                    FullName = name.FullName,
                    DateTime = dateTime,
                    Сategories = ListTypeExpense(dateTime, name.FullName, true),
                    Sum = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year &&
                                                         x.DateOperations.Month == dateTime.Month &&
                                                         x.UserFullName == name.FullName).Sum(x => x.Sum)
                };
                if (temporaryDRT.Sum!=0) listTD_RT.Add(temporaryDRT);
            }
            return listTD_RT;
        }

        public async Task<Report> ReportByNameUserYearMonth(string fullName, int year, string month)
        {
            DateTime dateTime = DefinePoly.DefinitionDateTime(year, month);
            var name = await _db.User.FirstOrDefaultAsync(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));
            Report temporaryDRT = new()
            {
                FullName = name.FullName,
                DateTime = dateTime,
                Сategories = ListTypeExpense(dateTime, name.FullName, true),
                Sum = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year &&
                                                     x.DateOperations.Month == dateTime.Month &&
                                                     x.UserFullName == name.FullName).Sum(x => x.Sum)
            };
            return temporaryDRT;
        }

        public async Task<List<string>> GetAllCategory()
        {
            var listCategory = await _db.Transaction.Select(x => x.Category).Distinct().ToArrayAsync();
            return _mapper.Map<List<string>>(listCategory);
        }
        public async Task<List<string>> GetAllFullNameUser()
        {
            var listFullName = await _db.Transaction.Select(x => x.UserFullName).Distinct().ToArrayAsync();
            return _mapper.Map<List<string>>(listFullName);
        }

        //--------------------------- Private method ---------------------------\\
        private List<TypeExpenseAndSum> ListTypeExpense(DateTime dateTime, string fullName, bool Month = false)
        {
            List<TypeExpenseAndSum> listTypeExpense = new List<TypeExpenseAndSum>();
            List<string> listCategory = _db.Transaction.Select(x => x.Category).Distinct().ToList();

            foreach (string category in listCategory)
            {
                var typeEAS = new TypeExpenseAndSum();
                typeEAS.NameTypeExpense = category;
                if (Month)
                {
                    typeEAS.Recipients = ListRecipient(dateTime, category, fullName, true);

                    typeEAS.SumTypeExpense = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year &&
                        x.DateOperations.Month == dateTime.Month && x.Category == category && x.UserFullName == fullName).Sum(x => x.Sum);
                }
                else
                {
                    typeEAS.Recipients = ListRecipient(dateTime, category, fullName);

                    typeEAS.SumTypeExpense = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year &&
                        x.Category == category && x.UserFullName == fullName).Sum(x => x.Sum);
                }
                if (typeEAS.SumTypeExpense!=0)
                    listTypeExpense.Add(typeEAS);
            }
            return listTypeExpense;
        }
        private List<ReportRecipient> ListRecipient(DateTime dateTime, string category, string fullName, bool Month = false)
        {
            List<ReportRecipient> listRecipientData = new();

            List<string> listRecipientName = _db.Transaction.Select(x => x.RecipientName).Distinct().ToList();

            foreach (var item in listRecipientName)
            {
                ReportRecipient recipient = new();
                recipient.NameRecipient = item;
                if (Month)
                    recipient.NameRecipientSum = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year &&
                    x.DateOperations.Month == dateTime.Month && x.Category == category && x.RecipientName == item && x.UserFullName == fullName).Sum(x => x.Sum);
                else
                    recipient.NameRecipientSum = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year &&
                    x.Category == category && x.RecipientName == item && x.UserFullName == fullName).Sum(x => x.Sum);
                if (recipient.NameRecipientSum!=0)
                    listRecipientData.Add(recipient);
            }
            return listRecipientData;
        }

    }
}
