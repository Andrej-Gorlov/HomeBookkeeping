using System.ComponentModel.DataAnnotations;

namespace HomeBookkeeping.Web.Models.HomeBookkeeping
{
    public class TemporaryDataReportCategotyBase
    {
        [Display(Name = "Имя")] public string? FullName { get; set; }
        [Display(Name = "Период")] public DateTime DateTime { get; set; }
        [Display(Name = "Каткгория")] public string? Category { get; set; }
        [Display(Name = "Получатель")] public List<TemporaryDataRecipientBase>? recipientsData { get; set; }
        [Display(Name = "Итог")] public decimal Sum { get; set; }
    }
}
