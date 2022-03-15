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
        public TransactionController(ITransactionService transactionService, IUserService userService)
        {
            _transactionService= transactionService;
            _userService= userService;
        } 


        public IActionResult TransactionIndex()=> View();

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


        public async Task<IActionResult> TransactionAdd()
        {
            List<UserDTOBase> listUser = new();
            var respons = await _userService.GetUsersAsync<ResponseBase>();
            if (respons.Result != null)
            {
                listUser = JsonConvert.DeserializeObject<List<UserDTOBase>>(Convert.ToString(respons.Result));
            }
            TransactionVM transactionVM = new()
            {
                Transaction = new()
                {
                    DateOperations=DateTime.Now
                },
                FullNameList = listUser.Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.FullName
                }).DistinctBy(x => x.Text),
                NumberCardList= listUser.SelectMany(x=>x.СreditСards).Select(y => new SelectListItem
                {
                    Text = y.Number,
                    Value = y.Number
                }).DistinctBy(x => x.Text),
            };
            return View(transactionVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionAdd(TransactionVM model)
        {
            if (ModelState.IsValid)
            {
                var respons = await _transactionService.AddTransactionAsync<ResponseBase>(model.Transaction);
                if (respons != null)
                    return RedirectToAction(nameof(TransactionGet));
            }
            return View(model);
        }


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
            if (ModelState.IsValid)
            {
                var respons = await _transactionService.DeleteTransactionAsync<ResponseBase>(model.Id);
                return RedirectToAction(nameof(TransactionGet));
            }
            return View(model);
        }


        public IActionResult TransactionDeleteDateTime()=>View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionDeleteDateTime(TransactionDTOBase model)
        {
            if (ModelState.IsValid)
            {
                var respons = await _transactionService.DeleteTransactionAsync<ResponseBase>(
                    model.DateOperations.Year, model.DateOperations.Month, model.DateOperations.Day,
                    model.DateOperations.Hour, model.DateOperations.Minute, model.DateOperations.Second);
                return RedirectToAction(nameof(TransactionGet));
            }
            return View(model);
        }
    }
}
