using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.DTO
{
    public class TemporaryData_ReportTimeDTO
    {
        public string? FullName { get; set; }
        public DateTime DateTime { get; set; }
        public List<TypeExpenseAndSumDTO>? Category { get; set; }
        public decimal Sum { get; set; }
    }
    public class TypeExpenseAndSumDTO
    {
        public string? NameTypeExpense { get; set; }
        public List<TemporaryData_RecipientDataDTO>? Recipient { get; set; }
        public decimal SumTypeExpense { get; set; }
    }
}
