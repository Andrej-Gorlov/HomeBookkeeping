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
        ///     GET /report/listCategory
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [Route("listCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategory() => Ok(await _reportSer.ServiceGetAllCategory());
        /// <summary>
        /// Список всех пользователей совершившиx транзакции.
        /// </summary>
        /// <returns>Вывод списка.</returns>
        /// <remarks>
        ///
        ///     GET /report/listFullNameUser
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [Route("listFullNameUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AllFullNameUser() => Ok(await _reportSer.ServiceGetAllFullNameUser());


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
        [Route("ReportByNameUserYear/{fullName}/{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReportByNameUserYear(string fullName, int year)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            var report = await _reportSer.ServiceReportByNameUserYear(fullName, year);
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
        [Route("ReportByNameUserYearMonth/{fullName}/{year}/{month}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReportByNameUserYearMonth(string fullName, int year, string month)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            var report = await _reportSer.ServiceReportByNameUserYearMonth(fullName, year, month);
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
        [Route("ReportByCategoryNameUserYear/{category}/{fullName}/{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReportByCategoryNameUserYear(string category, string fullName, int year)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            var report = await _reportSer.ServiceReportByCategoryNameUserYear(category, fullName, year);
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
        [Route("ReportByCategoryNameUserYearMonth/{category}/{fullName}/{year}/{month}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReportByCategoryNameUserYearMonth(string category, string fullName, int year, string month)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            var report = await _reportSer.ServiceReportByCategoryNameUserYearMonth(category, fullName, year, month);
            if (report.Result == null) return BadRequest(report);
            return Ok(report);
        }


        /// <summary>
        /// Полный отчёт.
        /// </summary>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        ///
        ///     GET /report/full
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [Route("full")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExpensFullReport() => Ok(await _reportSer.ServiceFullReport());


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
        [Route("ReportAllYearsNameUser/{fullName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReportAllYearsNameUser(string fullName)
        {
            var report = await _reportSer.ServiceReportAllYearsNameUser(fullName);
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
        [Route("ReportByCategoryAllYears/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReportByCategoryAllYears(string category)
        {
            var report = await _reportSer.ServiceReportByCategoryAllYears(category);
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
        [Route("ReportByCategoryYaer/{category}/{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReportByCategoryYaer(string category, int year)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            var report = await _reportSer.ServiceReportByCategoryYaer(category, year);
            if (report.Result == null) return BadRequest(report);
            return Ok(report);
        }
    }
}
