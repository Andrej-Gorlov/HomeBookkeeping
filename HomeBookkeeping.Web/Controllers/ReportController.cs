using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.ViewModels;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static HomeBookkeeping.Web.Models.ViewModels.ReportVM;

namespace HomeBookkeeping.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportServise _reportService;
        private readonly ITransactionService _transactionService;
        public ReportController(IReportServise reportService, ITransactionService transactionService)
        {
            _reportService = reportService;
            _transactionService = transactionService;
        }

        public IActionResult ReportIndex()=>View();


        public async Task <IActionResult> DataInputNameYear()
        {
            List<TransactionDTOBase> listTransaction = new();
            var respons = await _transactionService.GetTransactionsAsync<ResponseBase>();
            if (respons != null)
            {
                listTransaction = JsonConvert.DeserializeObject<List<TransactionDTOBase>>(Convert.ToString(respons.Result));
            }
            ReportVM reportVM = new ReportVM()
            {
                YearList = listTransaction.Select(x=>new SelectListItem
                {
                    Text=x.DateOperations.Year.ToString(),
                }).DistinctBy(x => x.Text),
                FullNameList = listTransaction.Select(x=>new SelectListItem
                {
                    Text=x.UserFullName
                }).DistinctBy(x => x.Text)
            };
            return View(reportVM);
        }
        public async Task<IActionResult> DataInputNameYearMonth()
        {
            List<TransactionDTOBase> listTransaction = new();
            var respons = await _transactionService.GetTransactionsAsync<ResponseBase>();
            if (respons != null)
            {
                listTransaction = JsonConvert.DeserializeObject<List<TransactionDTOBase>>(Convert.ToString(respons.Result));
            }
            ReportVM reportVM = new ReportVM()
            {
                YearList = listTransaction.Select(x => new SelectListItem
                {
                    Text = x.DateOperations.Year.ToString(),
                }).DistinctBy(x => x.Text),
                FullNameList = listTransaction.Select(x => new SelectListItem
                {
                    Text = x.UserFullName
                }).DistinctBy(x => x.Text),
            };
            return View(reportVM);
        }
        public async Task<IActionResult> DataInputNameCategoryYear()
        {
            List<TransactionDTOBase> listTransaction = new();
            var respons = await _transactionService.GetTransactionsAsync<ResponseBase>();
            if (respons != null)
            {
                listTransaction = JsonConvert.DeserializeObject<List<TransactionDTOBase>>(Convert.ToString(respons.Result));
            }
            ReportVM reportVM = new ReportVM()
            {
                CategoryList = listTransaction.Select(x=>new SelectListItem
                {
                    Text=x.Category
                }).DistinctBy(x => x.Text),
                YearList = listTransaction.Select(x => new SelectListItem
                {
                    Text = x.DateOperations.Year.ToString(),
                }).DistinctBy(x => x.Text),
                FullNameList = listTransaction.Select(x => new SelectListItem
                {
                    Text = x.UserFullName
                }).DistinctBy(x => x.Text),
            };
            return View(reportVM);
        }
        public async Task<IActionResult> DataInputNameCategoryYearMonth()
        {
            List<TransactionDTOBase> listTransaction = new();
            var respons = await _transactionService.GetTransactionsAsync<ResponseBase>();
            if (respons != null)
            {
                listTransaction = JsonConvert.DeserializeObject<List<TransactionDTOBase>>(Convert.ToString(respons.Result));
            }
            ReportVM reportVM = new ReportVM()
            {
                CategoryList = listTransaction.Select(x => new SelectListItem
                {
                    Text = x.Category
                }).DistinctBy(x => x.Text),
                YearList = listTransaction.Select(x => new SelectListItem
                {
                    Text = x.DateOperations.Year.ToString(),
                }).DistinctBy(x => x.Text),
                FullNameList = listTransaction.Select(x => new SelectListItem
                {
                    Text = x.UserFullName
                }).DistinctBy(x => x.Text),
            };
            return View(reportVM);
        }

        //private async Task<IActionResult> Dat()
        //{
        //    List<TransactionDTOBase> listTransaction = new();
        //    var respons = await _transactionService.GetTransactionsAsync<ResponseBase>();
        //    if (respons != null)
        //    {
        //        listTransaction = JsonConvert.DeserializeObject<List<TransactionDTOBase>>(Convert.ToString(respons.Result));
        //    }
        //    ReportVM reportVM = new ReportVM()
        //    {
        //        CategoryList = listTransaction.Select(x => new SelectListItem
        //        {
        //            Text = x.Category
        //        }).DistinctBy(x => x.Text),
        //        YearList = listTransaction.Select(x => new SelectListItem
        //        {
        //            Text = x.DateOperations.Year.ToString(),
        //        }).DistinctBy(x => x.Text),
        //        FullNameList = listTransaction.Select(x => new SelectListItem
        //        {
        //            Text = x.UserFullName
        //        }).DistinctBy(x => x.Text),
        //    };
        //    return View(reportVM);
        //}
        //-----------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportExpens(ReportVM reportVM)
        {
            List<TemporaryDataReportTimeBase> listTDRT = new();
            if (reportVM.month == null)
            {
                var respons = await _reportService.ExpensNameYearReportAsync<ResponseBase>(reportVM.fullName, reportVM.year);
                if (respons != null)
                    listTDRT = JsonConvert.DeserializeObject<List<TemporaryDataReportTimeBase>>(Convert.ToString(respons.Result));
                return View(listTDRT);
            }
            if (reportVM.month != null)
            {
                TemporaryDataReportTimeBase TDRT = new();
                var respons = await _reportService.ExpensNameYearMonthReportAsync<ResponseBase>(reportVM.fullName, reportVM.year, reportVM.month);
                if (respons != null)
                    TDRT = JsonConvert.DeserializeObject<TemporaryDataReportTimeBase>(Convert.ToString(respons.Result));
                listTDRT.Add(TDRT);
                return View(listTDRT);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportExpensCategory(ReportVM reportVM)
        {
            List<TemporaryDataReportCategotyBase> listTDRC = new();
            if (reportVM.month == null)
            {
                var respons = await _reportService.ExpensNameCategoryYearReportAsync<ResponseBase>(reportVM.category, reportVM.fullName, reportVM.year);
                if (respons != null)
                    listTDRC = JsonConvert.DeserializeObject<List<TemporaryDataReportCategotyBase>>(Convert.ToString(respons.Result));
                return View(listTDRC);
            }
            if (reportVM.month != null)
            {
                TemporaryDataReportCategotyBase TDRC = new();
                var respons = await _reportService.ExpensNameCategoryYearMonthReportAsync<ResponseBase>(reportVM.category,reportVM.fullName, reportVM.year, reportVM.month);
                if (respons != null)
                    TDRC = JsonConvert.DeserializeObject<TemporaryDataReportCategotyBase>(Convert.ToString(respons.Result));
                listTDRC.Add(TDRC);
                return View(listTDRC);
            }
            return RedirectToAction("Index");
        }
    }
}

