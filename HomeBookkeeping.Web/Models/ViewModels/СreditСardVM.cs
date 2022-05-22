using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.Paging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeBookkeeping.Web.Models.ViewModels
{
    public class СreditСardVM
    {
        public СreditСardDTOBase? СreditСard { get; set; }
        public IEnumerable<SelectListItem>? FullNameList { get; set; }
        public List<СreditСardDTOBase>? CreditСards { get; set; }
        public PagedList? Paging { get; set; }
    }
}
