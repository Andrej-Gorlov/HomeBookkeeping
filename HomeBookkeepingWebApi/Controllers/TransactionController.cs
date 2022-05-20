using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Paging;
using HomeBookkeepingWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace HomeBookkeepingWebApi.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionSer;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TransactionController(ITransactionService transactionSer,IWebHostEnvironment webHostEnvironment)
        {
            _transactionSer = transactionSer;
            _webHostEnvironment = webHostEnvironment;
        } 

        /// <summary>
        /// Создание новой транзакции.
        /// </summary>
        /// <param name="transactionDTO"></param>
        /// <returns> Создаётся транзакция </returns>
        /// <remarks>
        /// Образец ввовда данных:
        ///
        ///     Свойство ["id"] указываться не обязательно.
        ///     
        ///     POST /transaction
        ///     
        ///     {
        ///       "id": 0,                                        /// id транзакции.
        ///       "userFullName": "string",                       /// Полное имя пользователя совершившего транзакцию.
        ///       "numberCardUser": "string",                     /// Номер карты с которой списаны денежные средства.
        ///       "recipientName": "string",                      /// Имя/Названия организации получателя.
        ///       "dateOperations": "2022-03-03T11:25:16.544Z",   /// Дата проведения транзакции.
        ///       "sum": 0,                                       /// Сумма транзакции.
        ///       "category": "string"                            /// Категория.
        ///     }
        ///
        /// </remarks>
        /// <response code="201"> Tранзакция создана. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        [HttpPost]
        [Route("transaction")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDTO transactionDTO)
        {
            var transaction = await _transactionSer.AddServiceAsync(transactionDTO);
            if (transaction.Result is null)
            {
                return BadRequest(transaction);
            }
            return CreatedAtAction(nameof(GetByIdTransaction), transactionDTO);
        }

        /// <summary>
        /// Создание новых транзакций из ExcelFail.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="userFullName"></param>
        /// <param name="numberCardUser"></param>
        /// <returns> Создаётся транзакции </returns>
        ///         /// <remarks>
        /// Образец запроса:
        /// 
        ///     POST /transaction/FileExcel/
        ///     
        ///        userFullName: "string"     // Полное имя пользователя совершившего транзакцию.
        ///        numberCardUser: "string"   // Номер карты с которой списаны/зачислены денежные средства.
        ///        
        ///     Request body
        ///     
        ///        fileExcel: .xls/.xlsx      // Выберите файл с расширением xls, xlsx,
        ///     
        /// </remarks>
        /// <response code="201"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        [HttpPost]
        [Route("transaction/{userFullName}/{numberCardUser}/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTransactionFromFileExcel(string userFullName, string numberCardUser, IFormFile file)
        {
            var transactions = await _transactionSer.AddFileExcelServiceAsync(file, userFullName, numberCardUser);
            if (transactions.Result is null)
            {
                return BadRequest(transactions); 
            }
            return CreatedAtAction(nameof(GetByIdTransaction), transactions);
        }

        /// <summary>
        /// Удаление транзакции по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Транзакция удаляется.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /transaction/{id}
        ///     
        ///        Id: 0   // Введите id транзакции, которую нужно удалить.
        ///     
        /// </remarks>
        /// <response code="204"> Транзакция удалён. (нет содержимого) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Транзакция c указанным id не найден. </response>
        [HttpDelete]
        [Route("transaction/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            }
            var transaction = await _transactionSer.DeleteServiceAsync(id);
            if (transaction.Result is false)
            {
                return NotFound(transaction);
            }
            return NoContent();
        }

        /// <summary>
        /// Удаление транзакции по дате и времени
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns>Транзакция удаляется.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /transaction/{year}/{month}/{day}/{hour}/{minute}/{second}
        ///     
        ///        year: 0      // Введите год транзакции, которую нужно удалить.
        ///        month: 0     // Введите месяц транзакции, которую нужно удалить.
        ///        day: 0       // Введите день транзакции, которую нужно удалить.
        ///        hour: 0      // Введите час транзакции, которую нужно удалить.
        ///        minute: 0    // Введите минуту транзакции, которую нужно удалить.
        ///        ysecond: 0   // Введите секунду транзакции, которую нужно удалить.
        ///     
        /// </remarks>
        /// <response code="204"> Транзакция удалён. (нет содержимого) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Транзакция c указанной датой и временем не найден. </response>
        [HttpDelete]
        [Route("transaction/{year}/{month}/{day}/{hour}/{minute}/{second}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTransaction(int year, int month, int day, int hour, int minute, int second)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            if (month <= 0) return BadRequest($"месяц: [{month}] не может быть меньше или равен нулю");
            if (day <= 0) return BadRequest($"день: [{day}] не может быть меньше или равен нулю");
            
            DateTime data = new DateTime(year, month, day, hour, minute, second);
            var transaction = await _transactionSer.DeleteServiceAsync(data);
            if (transaction.Result is false)
            {
                return NotFound(transaction);
            }
            return NoContent();
        }

        /// <summary>
        /// Список всех транзакций
        /// </summary>
        /// <returns>Вывод всех существующих транзакций</returns>
        /// <remarks>
        /// Образец выовда запроса:
        ///
        ///     GET /transactions
        ///     
        ///        PageNumber: Номер страницы   // Введите номер страницы, которую нужно показать с списоком всех транзакций.
        ///        PageSize: Размер страницы    // Введите размер страницы, какое количество транзакций нужно вывести.
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Список всех транзакций не найден. </response>
        [HttpGet]
        [Route("transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetСreditСards([FromQuery] PagingQueryParameters paging)
        {
            var transactions = await _transactionSer.GetServiceAsync(paging);
            if (transactions.Result is null)
            {
                return NotFound(transactions);
            }
            var metadata = new
            {
                transactions.Result.TotalCount,
                transactions.Result.PageSize,
                transactions.Result.CurrentPage,
                transactions.Result.TotalPages,
                transactions.Result.HasNext,
                transactions.Result.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(transactions);
        }

        /// <summary>
        /// Вывод транзакции по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Вывод данных транзакции</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /transaction/{id:int}
        ///     
        ///        Id: 0   // Введите id транзакции, которую нужно показать.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Транзакция не найдена </response>
        [HttpGet]
        [Route("transaction/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdTransaction(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            }
            var transaction = await _transactionSer.GetByIdServiceAsync(id);
            if (transaction.Result is null)
            {
                return NotFound(transaction);
            }
            return Ok(transaction);
        }
    }
}
