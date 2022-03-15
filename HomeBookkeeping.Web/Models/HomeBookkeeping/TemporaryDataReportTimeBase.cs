using System.ComponentModel.DataAnnotations;

namespace HomeBookkeeping.Web.Models.HomeBookkeeping
{
    public class TemporaryDataReportTimeBase
    {
        [Display(Name = "Имя")] public string? FullName { get; set; }
        [Display(Name = "Период")] public DateTime DateTime { get; set; }
        [Display(Name = "Категория")] public List<TypeExpenseAndSumBase>? Сategories { get; set; }
        [Display(Name = "Итог")] public decimal Sum { get; set; }
    }
    public class TypeExpenseAndSumBase
    {
        [Display(Name = "Категория")] public string? NameTypeExpense { get; set; }
        [Display(Name = "Получатель")] public List<TemporaryDataRecipientBase>? Recipients { get; set; }
        [Display(Name = "Итог")] public decimal SumTypeExpense { get; set; }
    }
}
