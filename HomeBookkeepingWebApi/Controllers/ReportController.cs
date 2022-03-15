using HomeBookkeepingWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookkeepingWebApi.Controllers
{
    [Route("api/report")]
    [Produces("application/json")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportSer;
        public ReportController(IReportService reportSer)=> _reportSer = reportSer;


        /// <summary>
        /// Список всех категорий.
        /// </summary>
        /// <returns>Вывод списка.</returns>
        /// <remarks>
        ///
        ///     GET /report
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [Route("listCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategory() => Ok(await _reportSer.Service_GetAllCategory());
        /// <summary>
        /// Список всех пользователей совершившиx транзакции.
        /// </summary>
        /// <returns>Вывод списка.</returns>
        /// <remarks>
        ///
        ///     GET /report
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [Route("listFullNameUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AllFullNameUser() => Ok(await _reportSer.Service_GetAllFullNameUser());


        /// <summary>
        /// Отчёт определенного пользователя за конкретный год.
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="year"></param>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /report
        ///     
        ///        fullName: Иван Иванов   // Введите полное имя пользователя, отчёт которого нужно показать.
        ///        year: int (2000)        // Введите год за который нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ExpensNameYearReport/{fullName}/{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExpensNameYearReport(string fullName, int year)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            var report = await _reportSer.Service_ExpensNameYear(fullName, year);
            if (report.Result == null) return BadRequest(report);
            return Ok(report);
        }


        /// <summary>
        /// Отчёт определенного пользователя за конкретный год и месяц.
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /report
        ///     
        ///        fullName: Иван Иванов             // Введите полное имя пользователя, отчёт которого нужно показать.
        ///        year: int (2000)                  // Введите год за который нужно паказать отчёт.
        ///        month: (январь или 01 или 1)      // Введите месяц за который нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ExpensNameYearMonthReport/{fullName}/{year}/{month}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExpensNameYearMonthReport(string fullName, int year, string month)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            var report = await _reportSer.Service_ExpensNameYearMonth(fullName, year, month);
            if (report.Result == null) return BadRequest(report);
            return Ok(report);
        }


        /// <summary>
        /// Отчёт по категории определенного пользователя за конкретный год.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="fullName"></param>
        /// <param name="year"></param>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        /// 
        /// Образец запроса:
        /// 
        ///     GET /report
        ///     
        ///        typeExpense: Коммунальные расходы   // Введите категорию за которую нужно паказать отчёт.
        ///        fullName: Иван Иванов               // Введите полное имя пользователя, отчёт которого нужно показать.
        ///        year: int (2000)                    // Введите год за который нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ExpensNameCategoryYearReport/{category}/{fullName}/{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExpensNameCategoryYearReport(string category, string fullName, int year)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            var report = await _reportSer.Service_ExpensNameCategoryYear(category, fullName, year);
            if (report.Result == null) return BadRequest(report);
            return Ok(report);
        }


        /// <summary>
        /// Отчёт по категории определенного пользователя за конкретный год и месяц.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="fullName"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        /// 
        /// Образец запроса:
        /// 
        ///     GET /report
        ///     
        ///        category: Коммунальные расходы   // Введите категорию за которую нужно паказать отчёт.
        ///        fullName: Иван Иванов               // Введите полное имя пользователя, отчёт которого нужно показать.
        ///        year: int (2000)                    // Введите год за который нужно паказать отчёт.
        ///        month: (январь или 01 или 1)        // Введите месяц за который нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ExpensNameCategoryYearMonthReport/{category}/{fullName}/{year}/{month}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExpensNameCategoryYearMonthReport(string category, string fullName, int year, string month)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            var report = await _reportSer.Service_ExpensNameCategoryYearMonth(category, fullName, year, month);
            if (report.Result == null) return BadRequest(report);
            return Ok(report);
        }


        /// <summary>
        /// Полный отчёт.
        /// </summary>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        ///
        ///     GET /report
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExpensFullReport() => Ok(await _reportSer.Service_ExpensFull());


        /// <summary>
        /// Отчёт за все года определенного пользователя
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /report
        ///     
        ///        fullName: Иван Иванов // Введите полное имя пользователя, которого нужно показать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Отчёт не найден. </response>
        [HttpGet]
        [Route("{fullName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExpensNameFullYearReport(string fullName)
        {
            var report = await _reportSer.Service_ExpensNameFullYear(fullName);
            if (report.Result == null) return BadRequest(report);
            return Ok(report);
        }


        /// <summary>
        /// Отчёт определенной категории за все года.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /report
        ///     
        ///        typeExpense: Коммунальные расходы   // Введите категорию за которую нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Отчёт не найден. </response>
        [HttpGet]
        [Route("{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExpensCategoryFullYaerReport(string category)
        {
            var report = await _reportSer.Service_ExpensCategoryFullYaer(category);
            if (report.Result == null) return BadRequest(report);
            return Ok(report);
        }


        /// <summary>
        /// Отчёт определенной категории за конкретный год.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="year"></param>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /report
        ///     
        ///        category: Коммунальные расходы   // Введите категорию за которую нужно паказать отчёт.
        ///        year: int (2000)                    // Введите год за который нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Отчёт не найден. </response>
        [HttpGet]
        [Route("{category}/{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExpensCategoryYaerReport(string category, int year)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            var report = await _reportSer.Service_ExpensCategoryYaer(category, year);
            if (report.Result == null) return BadRequest(report);
            return Ok(report);
        }
    }
}
