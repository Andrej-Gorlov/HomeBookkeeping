using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.Paging;
using HomeBookkeeping.Web.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HomeBookkeeping.Web.Models.ViewModels
{
    public class TransactionVM
    {
        public СreditСardDTOBase? СreditСard { get; set; }
        public TransactionDTOBase? Transaction { get; set; }
        public UserDTOBase? User { get; set; }
        public IEnumerable<SelectListItem>? FullNameList { get; set; }
        [Required(ErrorMessage = "Выберите файл Excel")]
        public IFormFile? fileExcel { get; set; }

        public PagedList? Paging { get; set; }
        public List<TransactionDTOBase>? TransactionsDTO { get; set; }
    }
}
