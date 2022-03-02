using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.Entity.TemporaryData
{
    public class TemporaryData_ReportCategoty
    {
        [Display(Name = "Имя")] public string? FullName { get; set; }
        [Display(Name = "Период")] public DateTime DateTime { get; set; }
        [Display(Name = "Каткгория")] public string? Category { get; set; }
        [Display(Name = "Получатель")] public List<TemporaryData_RecipientData>? recipientDatas { get; set; }
        [Display(Name = "Итог")] public decimal Sum { get; set; }
    }
}
