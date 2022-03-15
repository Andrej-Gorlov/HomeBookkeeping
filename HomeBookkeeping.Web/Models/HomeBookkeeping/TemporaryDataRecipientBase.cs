using System.ComponentModel.DataAnnotations;

namespace HomeBookkeeping.Web.Models.HomeBookkeeping
{
    public class TemporaryDataRecipientBase
    {
        [Display(Name = "Получатель")] public string? NameRecipient { get; set; }
        [Display(Name = "Итог")] public decimal NameRecipientSum { get; set; }
    }
}
