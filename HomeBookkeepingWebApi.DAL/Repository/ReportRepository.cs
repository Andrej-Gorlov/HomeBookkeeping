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

        public async Task<IEnumerable<TemporaryData_ReportCategotyDTO>> ExpensCategoryFullYaer(string category)
        {
            var listUser = await _db.User.Select(x => x.FullName).Distinct().ToListAsync();
            List<int> listYear = await _db.Transaction.Select(x => x.DateOperations.Year).Distinct().ToListAsync();
            var nameCategory = _db.Transaction.FirstOrDefault(x => x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""));
            List<TemporaryData_ReportCategoty> TemporaryDRC = new();
            foreach (var itemUser in listUser)
            {
                foreach (var itemYear in listYear)
                {
                    List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == itemYear).Select(x => x.DateOperations.Month).Distinct().ToList();
                    foreach (var itemMonth in listMonth)
                    {
                        DateTime dateTime = DefinePoly.DefinitionDateTime(itemYear, itemMonth.ToString());
                        TemporaryData_ReportCategoty temporaryDRC = new()
                        {
                            FullName = itemUser,
                            DateTime = dateTime,
                            Category = nameCategory.Category,
                            recipientDatas = ListRecipient(dateTime, nameCategory.Category, itemUser, true),
                            Sum = (decimal)_db.Transaction.Where(x => x.UserFullName == itemUser &&
                                                           x.DateOperations.Year == dateTime.Year &&
                                                           x.DateOperations.Month == dateTime.Month &&
                                                           x.Category == nameCategory.Category).Sum(x => x.Sum)
                        };
                        TemporaryDRC.Add(temporaryDRC);
                    }
                }
            }
            return _mapper.Map<List<TemporaryData_ReportCategotyDTO>>(TemporaryDRC);
        }

        public async Task<IEnumerable<TemporaryData_ReportCategotyDTO>> ExpensCategoryYaer(string category, int year)
        {
            var nameCategory = _db.Transaction.FirstOrDefault(x => x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""));
            var listUser = await _db.User.Select(x => x.FullName).Distinct().ToListAsync();
            List<TemporaryData_ReportCategoty> TemporaryDRC = new();
            foreach (var itemUser in listUser)
            {
                List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == year).Select(x => x.DateOperations.Month).Distinct().ToList();
                foreach (var itemMonth in listMonth)
                {
                    DateTime dateTime = DefinePoly.DefinitionDateTime(year, itemMonth.ToString());
                    TemporaryData_ReportCategoty temporaryDRC = new()
                    {
                        FullName = itemUser,
                        DateTime = dateTime,
                        Category = nameCategory.Category,
                        recipientDatas = ListRecipient(dateTime, nameCategory.Category, itemUser, true),
                        Sum = (decimal)_db.Transaction.Where(x => x.UserFullName == itemUser &&
                                                       x.DateOperations.Year == dateTime.Year &&
                                                       x.DateOperations.Month == dateTime.Month &&
                                                       x.Category == nameCategory.Category).Sum(x => x.Sum)
                    };
                    TemporaryDRC.Add(temporaryDRC);
                }
            }
            return _mapper.Map<List<TemporaryData_ReportCategotyDTO>>(TemporaryDRC);
        }

        public async Task<IEnumerable<TemporaryData_ReportTimeDTO>> ExpensFull()
        {
            var listUser = await _db.User.Select(x => x.FullName).Distinct().ToListAsync();
            List<int> listYear = await _db.Transaction.Select(x => x.DateOperations.Year).Distinct().ToListAsync();
            List<TemporaryData_ReportTime> temporaryDRT = new();
            foreach (var itemName in listUser)
            {
                foreach (var itemYear in listYear)
                {
                    List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == itemYear).Select(x => x.DateOperations.Month).Distinct().ToList();
                    foreach (var itemMonth in listMonth)
                    {
                        DateTime dateTime = DefinePoly.DefinitionDateTime(itemYear, itemMonth.ToString());
                        TemporaryData_ReportTime temDRT = new()
                        {
                            FullName = itemName,
                            DateTime = dateTime,
                            Category = ListTypeExpense(dateTime, itemName, true),
                            Sum = (decimal)_db.Transaction.Where(
                                x => x.DateOperations.Year == dateTime.Year
                                && x.DateOperations.Month == dateTime.Month
                                && x.UserFullName == itemName).Sum(x => x.Sum)
                        };
                        temporaryDRT.Add(temDRT);
                    }
                }
            }
            return _mapper.Map<List<TemporaryData_ReportTimeDTO>>(temporaryDRT);
        }

        public async Task<IEnumerable<TemporaryData_ReportCategotyDTO>> ExpensNameCategoryYear(string category, string fullName, int year)
        {
            var name = await _db.User.FirstOrDefaultAsync(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));
            List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == year).Select(x => x.DateOperations.Month).Distinct().ToList();
            List<TemporaryData_ReportCategoty> listTRC = new();
            foreach (var item in listMonth)
            {
                DateTime dateTime = DefinePoly.DefinitionDateTime(year, item.ToString());
                TemporaryData_ReportCategoty tDRC = new()
                {
                    FullName = name.FullName,
                    DateTime = dateTime,
                    Category = category,
                    recipientDatas = ListRecipient(dateTime, category, name.FullName),
                    Sum = (decimal)_db.Transaction.Where(
                        x => x.UserFullName == name.FullName &&
                        x.DateOperations.Year == dateTime.Year &&
                        x.DateOperations.Month == dateTime.Month &&
                        x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""))
                        .Sum(x => x.Sum)
                };
                listTRC.Add(tDRC);
            }
            return _mapper.Map<List<TemporaryData_ReportCategotyDTO>>(listTRC);
        }

        public async Task<TemporaryData_ReportCategotyDTO> ExpensNameCategoryYearMonth(string category, string fullName, int year, string month)
        {
            DateTime dateTime = DefinePoly.DefinitionDateTime(year, month);
            var name = await _db.User.FirstOrDefaultAsync(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));
            TemporaryData_ReportCategoty tDRC = new()
            {
                FullName = name.FullName,
                DateTime = dateTime,
                Category = category,
                recipientDatas = ListRecipient(dateTime, category, name.FullName, true),
                Sum = (decimal)_db.Transaction.Where(
                    x => x.UserFullName == name.FullName &&
                    x.DateOperations.Year == dateTime.Year &&
                    x.DateOperations.Month == dateTime.Month &&
                    x.Category.ToUpper().Replace(" ", "") == category.ToUpper().Replace(" ", ""))
                    .Sum(x => x.Sum)
            };
            return _mapper.Map<TemporaryData_ReportCategotyDTO>(tDRC);
        }

        public async Task<IEnumerable<TemporaryData_ReportTimeDTO>> ExpensNameFullYear(string fullName)
        {
            var name = await _db.User.FirstOrDefaultAsync(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));
            List<int> listYear = await _db.Transaction.Select(x => x.DateOperations.Year).Distinct().ToListAsync();
            List<TemporaryData_ReportTime> listTD_RT = new();
            foreach (var itemYear in listYear)
            {
                List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == itemYear).Select(x => x.DateOperations.Month).Distinct().ToList();
                foreach (var itemMonth in listMonth)
                {
                    DateTime dateTime = DefinePoly.DefinitionDateTime(itemYear, itemMonth.ToString());
                    TemporaryData_ReportTime temporaryDRT = new()
                    {
                        FullName = name.FullName,
                        DateTime = dateTime,
                        Category = ListTypeExpense(dateTime, name.FullName, true),
                        Sum = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year
                                                            && x.DateOperations.Month == dateTime.Month
                                                            && x.UserFullName == name.FullName).Sum(x => x.Sum)
                    };
                    listTD_RT.Add(temporaryDRT);
                }
            }
            return _mapper.Map<List<TemporaryData_ReportTimeDTO>>(listTD_RT);
        }

        public async Task<List<TemporaryData_ReportTimeDTO>> ExpensNameYear(string fullName, int year)
        {
            var name = await _db.User.FirstOrDefaultAsync(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));
            List<TemporaryData_ReportTime> listTD_RT = new ();
            List<int> listMonth = _db.Transaction.Where(x => x.DateOperations.Year == year).Select(x => x.DateOperations.Month).Distinct().ToList();
            foreach (var item in listMonth)
            {
                DateTime dateTime = DefinePoly.DefinitionDateTime(year, item.ToString());
                TemporaryData_ReportTime temporaryDRT = new()
                {
                    FullName = name.FullName,
                    DateTime = dateTime,
                    Category = ListTypeExpense(dateTime, name.FullName, true),
                    Sum = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year &&
                                                         x.DateOperations.Month == dateTime.Month &&
                                                         x.UserFullName == name.FullName).Sum(x => x.Sum)
                };
                listTD_RT.Add(temporaryDRT);
            }
            return _mapper.Map<List <TemporaryData_ReportTimeDTO>>(listTD_RT);
        }

        public async Task<TemporaryData_ReportTimeDTO> ExpensNameYearMonth(string fullName, int year, string month)
        {
            DateTime dateTime = DefinePoly.DefinitionDateTime(year, month);
            var name = await _db.User.FirstOrDefaultAsync(x => x.FullName.ToUpper().Replace(" ", "") == fullName.ToUpper().Replace(" ", ""));
            TemporaryData_ReportTime temporaryDRT = new()
            {
                FullName = name.FullName,
                DateTime = dateTime,
                Category = ListTypeExpense(dateTime, name.FullName, true),
                Sum = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year &&
                                                     x.DateOperations.Month == dateTime.Month &&
                                                     x.UserFullName == name.FullName).Sum(x => x.Sum)
            };
            return _mapper.Map<TemporaryData_ReportTimeDTO>(temporaryDRT);
        }

        public async Task<List<string>> GetAllCategory()
        {
            var listCategory = await _db.Transaction.Select(x => x.Category).Distinct().ToArrayAsync();
            return _mapper.Map<List<string>>(listCategory);
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
                    typeEAS.Recipient = ListRecipient(dateTime, category, fullName, true);

                    typeEAS.SumTypeExpense = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year &&
                        x.DateOperations.Month == dateTime.Month && x.Category == category && x.UserFullName == fullName).Sum(x => x.Sum);
                }
                else
                {
                    typeEAS.Recipient = ListRecipient(dateTime, category, fullName);

                    typeEAS.SumTypeExpense = (decimal)_db.Transaction.Where(x => x.DateOperations.Year == dateTime.Year &&
                        x.Category == category && x.UserFullName == fullName).Sum(x => x.Sum);
                }
                if (typeEAS.SumTypeExpense!=0)
                    listTypeExpense.Add(typeEAS);
            }
            return listTypeExpense;
        }
        private List<TemporaryData_RecipientData> ListRecipient(DateTime dateTime, string category, string fullName, bool Month = false)
        {
            List<TemporaryData_RecipientData> listRecipientData = new();

            List<string> listRecipientName = _db.Transaction/*.Where(x => x.DateOperations == dateTime)*/.Select(x => x.RecipientName).Distinct().ToList();

            foreach (var item in listRecipientName)
            {
                TemporaryData_RecipientData recipient = new();
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
