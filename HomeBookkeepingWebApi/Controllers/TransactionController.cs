using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookkeepingWebApi.Controllers
{
    [Route("api/transaction")]
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
        ///     POST /user
        ///
        /// </remarks>
        /// <response code="201"> Tранзакция создана. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDTO transactionDTO)
        {
            var transaction = await _transactionSer.Service_Add(transactionDTO);
            if (transaction.Result == null) return BadRequest(transaction);
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
        ///     DELETE /transaction
        ///     
        ///     {
        ///        "Id": int // Введите id транзакции, которую нужно удалить.
        ///     }
        ///     
        /// </remarks>
        /// <response code="204"> Транзакция удалён. (нет содержимого) </response>
        /// <response code="404"> Транзакция c указанным id не найден. </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            if (id <= 0) return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            var transaction = await _transactionSer.Service_Delete(id);
            if (transaction.Result == false) return NotFound(transaction);
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
        ///     DELETE /transaction
        ///     
        ///     {
        ///        "year": int (2000)  // Введите год транзакции, которую нужно удалить.
        ///        "month": int (1)    // Введите месяц транзакции, которую нужно удалить.
        ///        "day": int (1)      // Введите день транзакции, которую нужно удалить.
        ///        "hour": int (0)     // Введите час транзакции, которую нужно удалить.
        ///        "minute": int (0)   // Введите минуту транзакции, которую нужно удалить.
        ///        "ysecond": int (0)  // Введите секунду транзакции, которую нужно удалить.
        ///     }
        ///     
        /// </remarks>
        /// <response code="204"> Транзакция удалён. (нет содержимого) </response>
        /// <response code="404"> Транзакция c указанной датой и временем не найден. </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        [HttpDelete]
        [Route("{year}/{month}/{day}/{hour}/{minute}/{second}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTransaction(int year, int month, int day, int hour, int minute, int second)
        {
            if (year <= 0) return BadRequest($"год: [{year}] не может быть меньше или равен нулю");
            if (month <= 0) return BadRequest($"месяц: [{month}] не может быть меньше или равен нулю");
            if (day <= 0) return BadRequest($"день: [{day}] не может быть меньше или равен нулю");
            DateTime data = new DateTime(year, month, day, hour, minute, second);
            var transaction = await _transactionSer.Service_Delete(data);
            if (transaction.Result == false) return NotFound(transaction);
            return NoContent();
        }

        /// <summary>
        /// Список всех транзакций
        /// </summary>
        /// <returns>Вывод всех существующих транзакций</returns>
        /// <remarks>
        /// Образец выовда запроса:
        ///
        ///     GET /transaction
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetСreditСards() => Ok(await _transactionSer.Service_Get());
    }
}
