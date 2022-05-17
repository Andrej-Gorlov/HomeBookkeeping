using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookkeepingWebApi.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionSer;
        public TransactionController(ITransactionService transactionSer)=> _transactionSer = transactionSer;

        /// <summary>
        /// Создание новой транзакции.
        /// </summary>
        /// <param name="transactionDTO"></param>
        /// <returns>Создаётся транзакция</returns>
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
            return Ok(transactionDTO);
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
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Список всех транзакций не найден. </response>
        [HttpGet]
        [Route("transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetСreditСards()
        {
            var transactions = await _transactionSer.GetServiceAsync();
            if (transactions.Result is null)
            {
                return NotFound(transactions);
            }
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
