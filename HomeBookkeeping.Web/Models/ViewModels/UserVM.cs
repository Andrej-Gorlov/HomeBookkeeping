using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.Paging;

namespace HomeBookkeeping.Web.Models.ViewModels
{
    public class UserVM
    {
        public List<UserDTOBase>? Users { get; set; }
        public PagedList? Paging { get; set; }
    }
}
