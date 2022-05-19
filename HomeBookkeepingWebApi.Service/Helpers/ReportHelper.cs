using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity.TemporaryData;

namespace HomeBookkeepingWebApi.Service.Helpers
{
    public static class ReportHelper
    {
        public static DateTime DefinitionDateTime(int year, string month = "1")
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
        public static List<TypeExpenseAndSum> ListTypeExpense(List<TransactionDTO> transactions, DateTime dateTime, string fullName, bool Month = false)
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
        public static List<ReportRecipient> ListRecipient(List<TransactionDTO> transactions, DateTime dateTime, string category, string fullName, bool Month = false)
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
