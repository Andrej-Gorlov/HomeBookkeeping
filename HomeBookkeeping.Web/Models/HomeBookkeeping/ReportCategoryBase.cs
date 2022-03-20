using System.ComponentModel.DataAnnotations;

namespace HomeBookkeeping.Web.Models.HomeBookkeeping
{
    public class ReportCategoryBase
    {
        [Display(Name = "Имя")] public string? FullName { get; set; }
        [Display(Name = "Период")] public DateTime DateTime { get; set; }
        [Display(Name = "Каткгория")] public string? Category { get; set; }
        [Display(Name = "Получатель")] public List<ReportRecipientBase>? recipientsData { get; set; }
        [Display(Name = "Итог")] public decimal Sum { get; set; }
    }
}
