using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.DTO
{
    public class СreditСardDTO
    {
        public int СreditСardId { get; set; }
        public int UserId { get; set; }
        public string? CardName { get; set; }
        public string? UserFullName { get; set; }
        public string? Number { get; set; }
        public string? R_Account { get; set; }
        public decimal Sum { get; set; }
    }
}
