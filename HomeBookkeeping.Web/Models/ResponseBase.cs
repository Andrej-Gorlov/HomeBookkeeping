using HomeBookkeeping.Web.Models.Paging;
using HomeBookkeeping.Web.Services;

namespace HomeBookkeeping.Web.Models
{
    public class ResponseBase
    {
        public object? Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string>? ErrorMessages { get; set; }
        public PagedList? PagedList { get; set; }
    }
}
