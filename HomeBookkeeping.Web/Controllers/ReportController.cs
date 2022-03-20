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


        [HttpGet]
        public IActionResult ReportIndex()=>View();
        [HttpGet]
        public async Task <IActionResult> ParamsNameUserYear()=> View(await Data());
        [HttpGet]
        public async Task<IActionResult> ParamsNameUserYearMonth()=> View(await Data());
        [HttpGet]
        public async Task<IActionResult> ParamsNameCategoryNameUserYear()=> View(await Data());
        [HttpGet]
        public async Task<IActionResult> ParamsNameCategoryNameUserYearMonth()=> View(await Data());
        [HttpGet]
        public async Task<IActionResult> FullReport()=> await FullReport(new ReportVM());
        [HttpGet]
        public async Task<IActionResult> ParameterNameCategory() => View(await Data());
        [HttpGet]
        public async Task<IActionResult> ParamsCategoryYaer() => View(await Data());
        [HttpGet]
        public async Task<IActionResult> ParameterNameUser() => View(await Data());


        [HttpGet]
        private async Task<ReportVM> Data()
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
            return reportVM;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FullReport(ReportVM reportVM)
        {
            List<ReportBase> listTDRT = new();
            if (reportVM.fullName==null && reportVM.month==null && reportVM.year == 0)
            {
                var respons = await _reportService.FullReportAsync<ResponseBase>();
                if (respons != null)
                    listTDRT = JsonConvert.DeserializeObject<List<ReportBase>>(Convert.ToString(respons.Result));
                return View(listTDRT);
            }
            if (reportVM.month == null && reportVM.year==0)
            {
                var respons = await _reportService.ReportAllYearsNameUserAsync<ResponseBase>(reportVM.fullName);
                if (respons != null)
                    listTDRT = JsonConvert.DeserializeObject<List<ReportBase>>(Convert.ToString(respons.Result));
                return View(listTDRT);
            }
            if (reportVM.month == null)
            {
                var respons = await _reportService.ReportByNameUserAsync<ResponseBase>(reportVM.fullName, reportVM.year);
                if (respons != null)
                    listTDRT = JsonConvert.DeserializeObject<List<ReportBase>>(Convert.ToString(respons.Result));
                return View(listTDRT);
            }
            if (reportVM.fullName!=null && reportVM.year!=0 && reportVM.month!=null)
            {
                ReportBase TDRT = new();
                var respons = await _reportService.ReportByNameUserYearMonthAsync<ResponseBase>(reportVM.fullName, reportVM.year, reportVM.month);
                if (respons != null)
                    TDRT = JsonConvert.DeserializeObject<ReportBase>(Convert.ToString(respons.Result));
                listTDRT.Add(TDRT);
                return View(listTDRT);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportCategory(ReportVM reportVM)
        {
            List<ReportCategoryBase> listTDRC = new();
            if (reportVM.year==0 && reportVM.month==null && reportVM.fullName == null)
            {
                var respons = await _reportService.ReportByCategoryAllYearsAsync<ResponseBase>(reportVM.category);
                if (respons != null)
                    listTDRC = JsonConvert.DeserializeObject<List<ReportCategoryBase>>(Convert.ToString(respons.Result));
                return View(listTDRC);
            }
            if (reportVM.month == null && reportVM.fullName==null)
            {
                var respons = await _reportService.ReportByCategoryYaerAsync<ResponseBase>(reportVM.category, reportVM.year);
                if (respons != null)
                    listTDRC = JsonConvert.DeserializeObject<List<ReportCategoryBase>>(Convert.ToString(respons.Result));
                return View(listTDRC);
            }
            if (reportVM.month == null)
            {
                var respons = await _reportService.ReportByCategoryNameUserYearAsync<ResponseBase>(reportVM.category, reportVM.fullName, reportVM.year);
                if (respons != null)
                    listTDRC = JsonConvert.DeserializeObject<List<ReportCategoryBase>>(Convert.ToString(respons.Result));
                return View(listTDRC);
            }
            if(reportVM.category!=null && reportVM.fullName!=null && reportVM.year!=0 && reportVM.month != null)
            {
                ReportCategoryBase TDRC = new();
                var respons = await _reportService.ReportByCategoryNameUserYearMonthAsync<ResponseBase>(reportVM.category,reportVM.fullName, reportVM.year, reportVM.month);
                if (respons != null)
                    TDRC = JsonConvert.DeserializeObject<ReportCategoryBase>(Convert.ToString(respons.Result));
                listTDRC.Add(TDRC);
                return View(listTDRC);
            }
            return RedirectToAction("Index");
        }
    }
}

