using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.ViewModels;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using OfficeOpenXml;

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


        [HttpGet]
        public async Task<IActionResult> TransactionDownload() 
        {
            List<UserDTOBase> listUser = new();
            var respons = await _userService.GetUsersAsync<ResponseBase>();
            listUser = JsonConvert.DeserializeObject<List<UserDTOBase>>(Convert.ToString(respons.Result));
            ViewBag.UserList = listUser;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionDownload(TransactionVM model)
        {
            var responsCreditСard = await _creditСardService.GetByIdСreditСardAsync<ResponseBase>(model.СreditСard.СreditСardId);
            СreditСardDTOBase? creditСard = JsonConvert.DeserializeObject<СreditСardDTOBase>(Convert.ToString(responsCreditСard.Result));
            string? userFullName = creditСard.UserFullName;
            string? numberCardUser = creditСard.Number;


            //string webRootPath = _webHostEnvironment.WebRootPath;
            //string path = @"\fileexcel\";
            //string upload = webRootPath + path;//путь к img 
            //string feilName = model.fileExcel.FileName;
            //using (var fileStream = new FileStream(Path.Combine(upload, feilName), FileMode.Create))
            //{
            //    model.fileExcel.CopyTo(fileStream);
            //}


            //DateTime date1 = new DateTime(2015,7,20,18,30,01);
            //string s1 = "26.02.2022";
            //string s2 = "09:44";
            //int year= int.Parse(s1.Substring(s1.Length - 4));
            //int month = int.Parse(s1[3].ToString()+s1[4].ToString());
            //int dey = int.Parse(s1.Substring(0, 2));
            //int has = int.Parse(s2.Substring(0, 2));
            //int minute =int.Parse(s2.Substring(s2.Length - 2));
            //DateTime date2 = new DateTime(year, month, dey, has, minute, 01);




            var list = new List<TransactionDTOBase>();
            using (var stream = new MemoryStream())
            {
                await model.fileExcel.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Лист1"];

                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 12; row <= rowCount; row++)
                    {
                        string RowEnd = worksheet.Cells[row, 1].Value.ToString().Substring(0, 30);

                        if (RowEnd== "Реквизиты для перевода на счёт")
                            break;

                        bool isRow = worksheet.Cells[row, 1].Value.ToString().Trim() == "Продолжение на следующей странице" 
                            || worksheet.Cells[row, 1].Value.ToString().Trim()== "ДАТА ОПЕРАЦИИ (МСК)";//<-----------


                        if (!isRow) 
                        {
                            string data = worksheet.Cells[row, 1].Value.ToString().Trim();
                            string time = worksheet.Cells[row, 2].Value.ToString().Trim();
                            int year = int.Parse(data.Substring(data.Length - 4));
                            int month = int.Parse(data[3].ToString() + data[4].ToString());
                            int day = int.Parse(data.Substring(0, 2));
                            int hour = int.Parse(time.Substring(0, 2));
                            int minute = int.Parse(time.Substring(time.Length - 2));

                            list.Add(new TransactionDTOBase
                            {
                                UserFullName = userFullName,
                                NumberCardUser = numberCardUser,

                                DateOperations = new DateTime(year,month,day,hour,minute,01),

                                Category = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                RecipientName = worksheet.Cells[row + 1, 5].Value.ToString().Trim(),
                                Sum = decimal.Parse(worksheet.Cells[row, 17].Value.ToString().Trim())
                            });
                            row++;
                        }
                    }
                }
            }


            return View();
        }
    }
}
