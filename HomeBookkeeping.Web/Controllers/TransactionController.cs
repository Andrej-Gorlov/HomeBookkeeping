using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.ViewModels;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HomeBookkeeping.Web.Controllers
{
    public class TransactionController : Controller
    {

        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        private readonly IСreditСardService _creditСardService;
        public TransactionController(ITransactionService transactionService, IUserService userService, IСreditСardService creditСardService)
        {
            _transactionService= transactionService;
            _userService= userService;
            _creditСardService = creditСardService;
        }

        [HttpGet]
        public IActionResult TransactionIndex()=> View();
        [HttpGet]
        public async Task<IActionResult> TransactionGet()
        {
            List<TransactionDTOBase> listTransaction = new();
            var respons = await _transactionService.GetTransactionsAsync<ResponseBase>();
            if (respons.Result != null)
            {
                listTransaction = JsonConvert.DeserializeObject<List<TransactionDTOBase>>(Convert.ToString(respons.Result));
            }
            return View(listTransaction);
        }


        [HttpGet]
        public async Task<IActionResult> TransactionAdd()
        {
            List<UserDTOBase> listUser = new();
            var respons = await _userService.GetUsersAsync<ResponseBase>();
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
            var respons = await _creditСardService.GetСreditСardsAsync<ResponseBase>();
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
            return RedirectToAction(nameof(TransactionGet));
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
    }
}
