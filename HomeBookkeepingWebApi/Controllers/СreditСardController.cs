using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookkeepingWebApi.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    public class СreditСardController : ControllerBase
    {
        private readonly IСreditСardService _creditСardSer;
        public СreditСardController(IСreditСardService creditСardSer)=> _creditСardSer = creditСardSer;

        /// <summary>
        /// Список всех кредитных карт
        /// </summary>
        /// <returns>Вывод всех кредитных карт</returns>
        /// <remarks>
        /// Образец выовда запроса:
        ///
        ///     GET /creditcards
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Список всех кредитных карт не найден. </response>
        [HttpGet]
        [Route("creditcards")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetСreditСards()
        {
            var creditCards = await _creditСardSer.GetServiceAsync();
            if (creditCards.Result is null)
            {
                return NotFound(creditCards);
            }
            return Ok(creditCards);
        }

        /// <summary>
        /// Список всех кредитных карт принадлежащему пользователю
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>Вывод всех кредитных карт принадлежащему пользователю</returns>
        /// <remarks>
        /// Образец выовда запроса:
        ///
        ///     fullName: "string"   // Введите полное имя пользователя чьи кредитные карты нужно показать.
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Список всех кредитных карт принадлежащему пользователю не найден. </response>
        [HttpGet]
        [Route("creditcards/listСreditСardsUser/{fullName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetСreditСards(string fullName)
        {
            var creditCards = await _creditСardSer.GetServiceAsync(fullName);
            if (creditCards.Result is null)
            {
                return NotFound (creditCards);
            }
            return Ok(creditCards);
        }

        /// <summary>
        /// Вывод кредитной карты по id.
        /// </summary>
        /// <remarks>
        /// <param name="id"></param>
        /// <returns>Вывод данных кредитной карты</returns>
        /// Образец запроса:
        /// 
        ///     GET /creditcard/{id:int}
        ///     
        ///        СreditСardId: 0   // Введите id кредитной карты, которую нужно показать.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Кредитная карта не найдена </response>
        [HttpGet]
        [Route("creditcard/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdСreditСard(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            }
            var creditСard = await _creditСardSer.GetByIdServiceAsync(id);
            if (creditСard.Result is null)
            {
                return NotFound(creditСard);
            }
            return Ok(creditСard);
        }

        /// <summary>
        /// Создание новой кредитной карты.
        /// </summary>
        /// <param name="creditСardDTO"></param>
        /// <returns>Создаётся кредитная карта</returns>
        /// <remarks>
        /// 
        ///     Свойство ["сreditСardId" и "sum"] указываться не обязательно.
        ///     
        /// Образец ввовда данных:
        ///
        ///     POST /creditcard
        ///     
        ///     {
        ///         "сreditСardId": 0,         // id кредитной карты.
        ///         "userId": 0,               // id пользователя, которому принадлежит кредитная карта.
        ///         "cardName": "string",      // Hазвание кредитной карты.
        ///         "userFullName": "string",  // Полное имя пользователя, которому принадлежит кредитная карта.
        ///         "number": "string",        // Номер кредитной карты.
        ///         "r_Account": "string",     // Расчетный счёт крединой карты.
        ///         "sum": 0                   // Баланс кредитной карты.
        ///     }
        ///     
        /// </remarks>
        /// <response code="201"> Кредитная карта создана. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        [HttpPost]
        [Route("creditcard")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateСreditСard([FromBody] СreditСardDTO creditСardDTO)
        {
            var creditСard = await _creditСardSer.CreateServiceAsync(creditСardDTO);
            if (creditСard.Result is null)
            {
                return BadRequest(creditСard);
            }
            return CreatedAtAction(nameof(GetСreditСards), creditСardDTO);//(GetСreditСard)?
        }

        /// <summary>
        /// Редактирование кредитной карты.
        /// </summary>
        /// <param name="creditСardDTO"></param>
        /// <returns>Обновление кредитной карты</returns>
        /// <remarks>
        /// Образец ввовда данных:
        ///
        ///     PUT /creditcard
        ///     
        ///     {
        ///         "сreditСardId": 0,         // id кредитной карты.
        ///         "userId": 0,               // id пользователя, которому принадлежит кредитная карта.
        ///         "cardName": "string",      // Hазвание кредитной карты.
        ///         "userFullName": "string",  // Полное имя пользователя, которому принадлежит кредитная карта.
        ///         "number": "string",        // Номер кредитной карты.
        ///         "r_Account": "string",     // Расчетный счёт крединой карты.
        ///         "sum": 0                   // Баланс кредитной карты.
        ///     }
        ///
        /// </remarks>
        /// <response code="200"> Кредитная карта обновлена. </response>
        /// <response code="404"> Кредитная карта не найдена. </response>
        [HttpPut]
        [Route("creditcard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateСreditСard([FromBody] СreditСardDTO creditСardDTO)
        {
            var creditСard = await _creditСardSer.UpdateServiceAsync(creditСardDTO);
            if (creditСard.Result is null)
            {
                return NotFound(creditСard);
            }
            return Ok(creditСard);
        }

        /// <summary>
        /// Удаление кредитной карты.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Удаление кредитной карты.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /creditcard/{id}
        ///     
        ///        СreditСardId: 0   // Введите id кредитной карты, которую нужно удалить.
        ///     
        /// </remarks>
        /// <response code="204"> Кредитная карта удалена. (нет содержимого) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Кредитная карта c указанным id не найдена. </response>
        [HttpDelete]
        [Route("creditcard/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteСreditСard(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            }
            var сreditСard = await _creditСardSer.DeleteServiceAsync(id);
            if (сreditСard.Result is false)
            {
                return NotFound(сreditСard);
            }
            return NoContent();
        }

        /// <summary>
        /// Зачисление денежных средств на кредитную карту
        /// </summary>
        /// <param name="nameBank"></param>
        /// <param name="number"></param>
        /// <param name="sum"></param>
        /// <returns>Денежные средства зачислены на кредитную карту</returns>
        /// <remarks>
        /// 
        ///     GET /creditcard
        /// 
        /// Образец ввовда данных:
        /// 
        ///     nameBank: "string"   // Введите названия кредитной карты
        ///     numbe: "string"      // Введите номер кредитной карты
        ///     sum: 00.00           // Введите сумму для зачисления на кредитную карту
        /// 
        /// </remarks>
        /// <response code="201"> Денежные средства зачислены. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        /// <response code="404"> Кредитная карта не найдена. </response>
        [HttpPost]
        [Route("creditcard/{nameBank}/{number}/{sum}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EnrollmentСreditСard(string nameBank, string number, decimal sum)
        {
            if (sum < 0)
            {
                return BadRequest($"Сумма: [{sum}] для зачисления, не может быть меньше нуля");
            }
            var creditСard = await _creditСardSer.EnrollmentServiceAsync(nameBank, number, sum);
            if (creditСard.Result is null)
            {
                return NotFound(creditСard);
            }
            return CreatedAtAction(nameof(GetСreditСards), creditСard);
        }
    }
}
