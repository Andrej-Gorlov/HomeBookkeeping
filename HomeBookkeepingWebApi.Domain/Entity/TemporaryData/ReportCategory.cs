using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.Entity.TemporaryData
{
    public class ReportCategory
    {
        public string? FullName { get; set; }
        public DateTime DateTime { get; set; }
        public string? Category { get; set; }
        public List<ReportRecipient>? recipientsData { get; set; }
        public decimal Sum { get; set; }
    }
}
