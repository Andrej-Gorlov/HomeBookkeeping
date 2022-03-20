using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.Entity.TemporaryData
{
    public class ReportRecipient
    {
        public string? NameRecipient { get; set; }
        public decimal NameRecipientSum { get; set; }
    }
}
