using HomeBookkeeping.Web.Models.HomeBookkeeping;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeBookkeeping.Web.Models.ViewModels
{
    public class TransactionVM
    {
        public СreditСardDTOBase? СreditСard { get; set; }
        public TransactionDTOBase? Transaction { get; set; }
    }
}
