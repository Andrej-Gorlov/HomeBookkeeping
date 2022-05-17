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
        /// <response code="404"> Список всех категорий не найден. </response>
        [HttpGet]
        [Route("listCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCategory()
        {
            var catrgorys = await _reportSer.GetAllCategoryServiceAsync();
            if (catrgorys is null)
            {
                return NotFound();
            }
            return Ok(catrgorys);
        }
        
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
        /// <response code="404"> Список всех пользователей не найден. </response>
        [HttpGet]
        [Route("listFullNameUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AllFullNameUser()
        {
            var nameUsers = await _reportSer.GetAllFullNameUserServiceAsync();
            if (nameUsers is null)
            {
                return NotFound();
            }
            return Ok(nameUsers);
        } 

        /// <summary>
        /// Отчёт определенного пользователя за конкретный год.
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="year"></param>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /report/ReportByNameUserYear/{fullName}/{year}
        ///     
        ///        fullName: "string"   // Введите полное имя пользователя, отчёт которого нужно показать.
        ///        year: 0              // Введите год за который нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода. </response>
        /// <response code="404"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ReportByNameUserYear/{fullName}/{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReportByNameUserYear(string fullName, int year)
        {
            if (year <= 0)
            {
                return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            }
            var report = await _reportSer.ReportByNameUserYearServiceAsync(fullName, year);
            if (report.Result is null)
            {
                return NotFound();
            }
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
        ///     GET /report/ReportByNameUserYearMonth/{fullName}/{year}/{month}
        ///     
        ///        fullName: "string"   // Введите полное имя пользователя, отчёт которого нужно показать.
        ///        year: 0              // Введите год за который нужно паказать отчёт.
        ///        month: "string"      // Введите месяц за который нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода. </response>
        /// <response code="404"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ReportByNameUserYearMonth/{fullName}/{year}/{month}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReportByNameUserYearMonth(string fullName, int year, string month)
        {
            if (year <= 0)
            {
                return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            }
            var report = await _reportSer.ReportByNameUserYearMonthServiceAsync(fullName, year, month);
            if (report.Result is null)
            {
                return NotFound();
            };
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
        ///     GET /report/ReportByCategoryNameUserYear/{category}/{fullName}/{year}
        ///     
        ///        typeExpense: "string"   // Введите категорию за которую нужно паказать отчёт.
        ///        fullName: "string"      // Введите полное имя пользователя, отчёт которого нужно показать.
        ///        year: 0                 // Введите год за который нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода. </response>
        /// <response code="404"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ReportByCategoryNameUserYear/{category}/{fullName}/{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReportByCategoryNameUserYear(string category, string fullName, int year)
        {
            if (year <= 0)
            {
                return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            }
            var report = await _reportSer.ReportByCategoryNameUserYearServiceAsync(category, fullName, year);
            if (report.Result is null)
            {
                return NotFound();
            }
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
        ///     GET /report/ReportByCategoryNameUserYearMonth/{category}/{fullName}/{year}/{month}
        ///     
        ///        category: "string"       // Введите категорию за которую нужно паказать отчёт.
        ///        fullName: "string"       // Введите полное имя пользователя, отчёт которого нужно показать.
        ///        year: 0                  // Введите год за который нужно паказать отчёт.
        ///        month: "string"          // Введите месяц за который нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода. </response>
        /// <response code="404"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ReportByCategoryNameUserYearMonth/{category}/{fullName}/{year}/{month}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReportByCategoryNameUserYearMonth(string category, string fullName, int year, string month)
        {
            if (year <= 0)
            {
                return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            }
            var report = await _reportSer.ReportByCategoryNameUserYearMonthServiceAsync(category, fullName, year, month);
            if (report.Result is null)
            {
                return NotFound();
            }
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
        /// <response code="404"> Отчёт не найден. </response>
        [HttpGet]
        [Route("full")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExpensFullReport()
        {
            var report = await _reportSer.FullReportServiceAsync();
            if (report.Result is null)
            {
                return NotFound();
            }
            return Ok(report);
        } 

        /// <summary>
        /// Отчёт за все года определенного пользователя
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>Вывод отчёта.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /report/ReportAllYearsNameUser/{fullName}
        ///     
        ///        fullName: "string"   // Введите полное имя пользователя, которого нужно показать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ReportAllYearsNameUser/{fullName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReportAllYearsNameUser(string fullName)
        {
            var report = await _reportSer.ReportAllYearsNameUserServiceAsync(fullName);
            if (report.Result is null)
            {
                return NotFound();
            }
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
        ///     GET /report/ReportByCategoryAllYears/{category}
        ///     
        ///        typeExpense: "string"   // Введите категорию за которую нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ReportByCategoryAllYears/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReportByCategoryAllYears(string category)
        {
            var report = await _reportSer.ReportByCategoryAllYearsServiceAsync(category);
            if (report.Result is null)
            {
                return NotFound();
            }
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
        ///     GET /report/ReportByCategoryYaer/{category}/{year}
        ///     
        ///        category: "string"         // Введите категорию за которую нужно паказать отчёт.
        ///        year: 0                    // Введите год за который нужно паказать отчёт.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода. </response>
        /// <response code="404"> Отчёт не найден. </response>
        [HttpGet]
        [Route("ReportByCategoryYaer/{category}/{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReportByCategoryYaer(string category, int year)
        {
            if (year <= 0)
            {
                return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            }
            var report = await _reportSer.ReportByCategoryYaerServiceAsync(category, year);
            if (report.Result is null)
            {
                return NotFound();
            }
            return Ok(report);
        }
    }
}
