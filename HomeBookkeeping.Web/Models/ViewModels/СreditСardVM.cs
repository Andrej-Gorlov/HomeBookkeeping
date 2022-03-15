using HomeBookkeeping.Web.Models.HomeBookkeeping;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeBookkeeping.Web.Models.ViewModels
{
    public class СreditСardVM
    {
        public СreditСardDTOBase? СreditСard { get; set; }
        public IEnumerable<SelectListItem>? FullNameList { get; set; }
    }
}
