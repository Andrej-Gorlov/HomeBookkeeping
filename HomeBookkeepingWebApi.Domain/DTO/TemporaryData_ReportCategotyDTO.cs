using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.DTO
{
    public class TemporaryData_ReportCategotyDTO
    {
        public string? FullName { get; set; }
        public DateTime DateTime { get; set; }
        public string? Category { get; set; }
        public List<TemporaryData_RecipientDataDTO>? recipientDatas { get; set; }
        public decimal Sum { get; set; }
    }
}
