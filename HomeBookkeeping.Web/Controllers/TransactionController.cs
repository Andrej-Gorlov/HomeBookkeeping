using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.Paging;
using HomeBookkeeping.Web.Models.ViewModels;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeBookkeeping.Web.Controllers
{
    public class TransactionController : Controller
    {

        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        private readonly IСreditСardService _creditСardService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TransactionController(ITransactionService transactionService, IUserService userService, IСreditСardService creditСardService, IWebHostEnvironment webHostEnvironment)
        {
            _transactionService= transactionService;
            _userService= userService;
            _creditСardService = creditСardService;
            _webHostEnvironment= webHostEnvironment;
        }

        [HttpGet]
        public IActionResult TransactionIndex()=> View();
        [HttpGet]
        public async Task<IActionResult> TransactionGet(int page = 1)
        {
            TransactionVM transactionVM = new();
            var respons = await _transactionService.GetTransactionsAsync<ResponseBase>(new PagingParameters() { PageNumber =page});
            if (respons.Result != null)
            {
                transactionVM.TransactionsDTO = JsonConvert.DeserializeObject<List<TransactionDTOBase>>(Convert.ToString(respons.Result));
                transactionVM.Paging = respons.PagedList;
            }
            return View(transactionVM);
        }


        [HttpGet]
        public async Task<IActionResult> TransactionAdd()
        {
            List<UserDTOBase> listUser = new();
            var respons = await _userService.GetUsersAsync<ResponseBase>(new PagingParameters() { PageSize=1000 });
            listUser = JsonConvert.DeserializeObject<List<UserDTOBase>>(Convert.ToString(respons.Result));
            ViewBag.UserList = listUser;
            TransactionVM transactionVM = new()
            {
                Transaction = new()
                {
                    DateOperations=DateTime.Now
                }
            };
            return View(transactionVM);
        }
        [HttpGet]
        public async Task<JsonResult> GetСreditСardByUserIdAsync(int userId)
        {
            List<СreditСardDTOBase> listСreditСard = new();
            var respons = await _creditСardService.GetСreditСardsAsync<ResponseBase>(new PagingParameters() { PageSize = 1000 });
            listСreditСard = JsonConvert.DeserializeObject<List<СreditСardDTOBase>>(Convert.ToString(respons.Result));
            var data= listСreditСard.Where(x=>x.UserId== userId).Select(x => new
            {
                Id=x.СreditСardId,
                Number= x.Number,
                User = x.UserFullName,
            }).ToList();
            return Json(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionAdd(TransactionVM model)
        {
            var responsCreditСard = await _creditСardService.GetByIdСreditСardAsync<ResponseBase>(model.СreditСard.СreditСardId);
            СreditСardDTOBase? creditСard = JsonConvert.DeserializeObject<СreditСardDTOBase>(Convert.ToString(responsCreditСard.Result));
            model.Transaction.UserFullName=creditСard.UserFullName;
            model.Transaction.NumberCardUser = creditСard.Number;
            var responsTransaction = await _transactionService.AddTransactionAsync<ResponseBase>(model.Transaction);
            if (responsTransaction != null)
            {
                return RedirectToAction(nameof(TransactionGet));
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> TransactionDelete(int transactionId)
        {
            var respons = await _transactionService.GetByIdTransactionAsync<ResponseBase>(transactionId);
            if (respons != null)
            {
                TransactionDTOBase? model = JsonConvert.DeserializeObject<TransactionDTOBase>(Convert.ToString(respons.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionDelete(TransactionDTOBase model)
        {
            var respons = await _transactionService.DeleteTransactionAsync<ResponseBase>(model.Id);
            return RedirectToAction(nameof(TransactionGet));
        }


        [HttpGet]
        public IActionResult TransactionDeleteDateTime()=>View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionDeleteDateTime(TransactionDTOBase model)
        {
            var respons = await _transactionService.DeleteTransactionAsync<ResponseBase>(
            model.DateOperations.Year, model.DateOperations.Month, model.DateOperations.Day,
            model.DateOperations.Hour, model.DateOperations.Minute, model.DateOperations.Second);
            return RedirectToAction(nameof(TransactionGet));
        }


        [HttpGet]
        public async Task<IActionResult> TransactionDownload() 
        {
            List<UserDTOBase> listUser = new();
            var respons = await _userService.GetUsersAsync<ResponseBase>(new PagingParameters() { PageSize = 1000 });
            listUser = JsonConvert.DeserializeObject<List<UserDTOBase>>(Convert.ToString(respons.Result));
            ViewBag.UserList = listUser;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionDownloadFile(TransactionVM model)
        {
            var responsCreditСard = await _creditСardService.GetByIdСreditСardAsync<ResponseBase>(model.СreditСard.СreditСardId);
            СreditСardDTOBase? creditСard = JsonConvert.DeserializeObject<СreditСardDTOBase>(Convert.ToString(responsCreditСard.Result));
            string? userFullName = creditСard.UserFullName;
            string? numberCardUser = creditСard.Number;
            var responsTransaction = await _transactionService.AddTransactionFromFileExcelAsync<ResponseBase>(model.fileExcel, userFullName, numberCardUser);
            if (responsTransaction != null)
            {
                return RedirectToAction(nameof(TransactionGet));
            }
            return View(model);
        }
    }
}
