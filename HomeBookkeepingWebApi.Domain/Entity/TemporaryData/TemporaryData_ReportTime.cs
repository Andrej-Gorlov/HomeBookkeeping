using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.Entity.TemporaryData
{
    public class TemporaryData_ReportTime
    {
        [Display(Name = "Имя")] public string? FullName { get; set; }
        [Display(Name = "Период")] public DateTime DateTime { get; set; }
        [Display(Name = "Категория")] public List<TypeExpenseAndSum>? Category { get; set; }
        [Display(Name = "Итог")] public decimal Sum { get; set; }
    }
    public class TypeExpenseAndSum
    {
        [Display(Name = "Название категории")] public string? NameTypeExpense { get; set; }
        [Display(Name = "Получатель")] public List<TemporaryData_RecipientData>? Recipient { get; set; }
        [Display(Name = "Итог по категории")] public decimal SumTypeExpense { get; set; }
    }

}
