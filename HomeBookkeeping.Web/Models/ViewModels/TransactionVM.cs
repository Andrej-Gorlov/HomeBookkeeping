using HomeBookkeeping.Web.Models.HomeBookkeeping;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeBookkeeping.Web.Models.ViewModels
{
    public class TransactionVM
    {
        public TransactionDTOBase? Transaction { get; set; }
        public IEnumerable<SelectListItem>? FullNameList { get; set; }
        public IEnumerable<SelectListItem>? NumberCardList { get; set; }
        //public IEnumerable<SelectListItem>? CategoryList { get; set; }
        //public int year { get; set; }
    }
}
