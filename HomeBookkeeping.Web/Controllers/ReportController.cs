using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.Paging;
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
        private ReportVM riv = new();
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
        public async Task<IActionResult> FullReport(int page = 1) => await FullReport(page,riv);
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
            var respons = await _transactionService.GetTransactionsAsync<ResponseBase>(new PagingParameters() { PageSize=1000});

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
        public async Task<IActionResult> FullReport(int page, ReportVM report)
        {
            if (page == 0)
                page++;

            if (report.fullName==null && report.month==null && report.year == 0)
            {
                var respons = await _reportService.FullReportAsync<ResponseBase>(new PagingParameters() { PageNumber = page });
                if (respons != null)
                {
                    report.Reports = JsonConvert.DeserializeObject<List<ReportBase>>(Convert.ToString(respons.Result));
                    report.Paging = respons.PagedList;
                }   
                return View(report);
            }
            if (report.month == null && report.year==0)
            {
                var respons = await _reportService.ReportAllYearsNameUserAsync<ResponseBase>(new PagingParameters() { PageNumber = page }, report.fullName);
                if (respons != null)
                {
                    report.Reports = JsonConvert.DeserializeObject<List<ReportBase>>(Convert.ToString(respons.Result));
                    report.Paging = respons.PagedList;
                }  
                return View(report);
            }
            if (report.month == null)
            {
                var respons = await _reportService.ReportByNameUserAsync<ResponseBase>(new PagingParameters() { PageNumber = page }, report.fullName, report.year);
                if (respons != null)
                {
                    report.Reports = JsonConvert.DeserializeObject<List<ReportBase>>(Convert.ToString(respons.Result));
                    report.Paging = respons.PagedList;
                }  
                return View(report);
            }
            if (report.fullName!=null && report.year!=0 && report.month!=null)
            {
                ReportBase TDRT = new();
                var respons = await _reportService.ReportByNameUserYearMonthAsync<ResponseBase>(report.fullName, report.year, report.month);
                if (respons != null)
                    TDRT = JsonConvert.DeserializeObject<ReportBase>(Convert.ToString(respons.Result));
                report.Reports.Add(TDRT);
                return View(report);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportCategory(int page,ReportVM reportVM)
        {
            if (page == 0)
                page++;

            ReportVM report = new();
            if (reportVM.year==0 && reportVM.month==null && reportVM.fullName == null)
            {
                var respons = await _reportService.ReportByCategoryAllYearsAsync<ResponseBase>(new PagingParameters() { PageNumber = page }, reportVM.category);
                if (respons != null)
                {
                    report.ReportCategories = JsonConvert.DeserializeObject<List<ReportCategoryBase>>(Convert.ToString(respons.Result));
                    report.Paging = respons.PagedList;
                }
                return View(report);
            }
            if (reportVM.month == null && reportVM.fullName==null)
            {
                var respons = await _reportService.ReportByCategoryYaerAsync<ResponseBase>(new PagingParameters() { PageNumber = page }, reportVM.category, reportVM.year);
                if (respons != null)
                {
                    report.ReportCategories = JsonConvert.DeserializeObject<List<ReportCategoryBase>>(Convert.ToString(respons.Result));
                    report.Paging = respons.PagedList;
                }
                return View(report);
            }
            if (reportVM.month == null)
            {
                var respons = await _reportService.ReportByCategoryNameUserYearAsync<ResponseBase>(new PagingParameters() { PageNumber = page }, reportVM.category, reportVM.fullName, reportVM.year);
                if (respons != null)
                {
                    report.ReportCategories = JsonConvert.DeserializeObject<List<ReportCategoryBase>>(Convert.ToString(respons.Result));
                    report.Paging = respons.PagedList;
                }
                return View(report);
            }
            if(reportVM.category!=null && reportVM.fullName!=null && reportVM.year!=0 && reportVM.month != null)
            {
                ReportCategoryBase TDRC = new();
                var respons = await _reportService.ReportByCategoryNameUserYearMonthAsync<ResponseBase>(reportVM.category,reportVM.fullName, reportVM.year, reportVM.month);
                if (respons != null)
                    TDRC = JsonConvert.DeserializeObject<ReportCategoryBase>(Convert.ToString(respons.Result));
                report.ReportCategories.Add(TDRC);
                return View(report);
            }
            return RedirectToAction("Index");
        }
    }
}

