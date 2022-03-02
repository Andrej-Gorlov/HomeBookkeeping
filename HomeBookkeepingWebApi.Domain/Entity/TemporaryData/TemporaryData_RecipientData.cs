using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.Entity.TemporaryData
{
    public class TemporaryData_RecipientData
    {
        [Display(Name = "Название получателя")] public string? NameRecipient { get; set; }
        [Display(Name = "Итог по получателю")] public decimal NameRecipientSum { get; set; }
    }
}
