using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookkeepingWebApi.Controllers
{
    [Route("api/creditcard")]
    [Produces("application/json")]
    public class СreditСardController : ControllerBase
    {
        private readonly IСreditСardService _creditСardSer;
        public СreditСardController(IСreditСardService creditСardSer)=> _creditСardSer = creditСardSer;

        /// <summary>
        /// Список всех кредитных карт
        /// </summary>
        /// <returns>Вывод всех существующих кредитных карт</returns>
        /// <remarks>
        /// Образец выовда запроса:
        ///
        ///     GET /creditcard
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetСreditСards() => Ok(await _creditСardSer.Service_Get());

        /// <summary>
        /// Вывод кредитной карты по id.
        /// </summary>
        /// <remarks>
        /// <param name="id"></param>
        /// <returns>Вывод данных кредитной карты</returns>
        /// Образец запроса:
        /// 
        ///     GET /creditcard
        ///     
        ///     {
        ///        "СreditСardId": int // Введите id кредитной карты, которую нужно показать.
        ///     }
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Кредитная карта не найдена </response>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdСreditСard(int id)
        {
            if (id <= 0) return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            var creditСard = await _creditСardSer.Service_GetById(id);
            if (creditСard.Result == null) return BadRequest(creditСard);
            return Ok(creditСard);
        }

        /// <summary>
        /// Создание новой кредитной карты.
        /// </summary>
        /// <param name="creditСardDTO"></param>
        /// <returns>Создаётся кредитная карта</returns>
        /// <remarks>
        /// Образец ввовда данных:
        ///
        ///     POST /creditcard
        ///
        /// </remarks>
        /// <response code="201"> Кредитная карта создана. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateСreditСard([FromBody] СreditСardDTO creditСardDTO)
        {
            var creditСard = await _creditСardSer.Service_Create(creditСardDTO);
            if (creditСard.Result == null) return BadRequest(creditСard); //
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
        /// </remarks>
        /// <response code="200"> Кредитная карта обновлена. </response>
        /// <response code="404"> Кредитная карта не найдена. </response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateСreditСard([FromBody] СreditСardDTO creditСardDTO)
        {
            var creditСard = await _creditСardSer.Service_Update(creditСardDTO);
            if (creditСard.Result == null) return BadRequest(creditСard);
            return Ok(creditСard);
        }

        /// <summary>
        /// Удаление кредитной карты.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Кредитная карта удаляется.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /creditcard
        ///     
        ///     {
        ///        "СreditСardId": int // Введите id кредитной карты, которую нужно удалить.
        ///     }
        ///     
        /// </remarks>
        /// <response code="204"> Кредитная карта удалена. (нет содержимого) </response>
        /// <response code="404"> Кредитная карта c указанным id не найдена. </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteСreditСard(int id)
        {
            if (id <= 0) return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            var сreditСard = await _creditСardSer.Service_Delete(id);
            if (сreditСard.Result == false) return NotFound(сreditСard);
            return NoContent();
        }

        /// <summary>
        /// Зачисление денежных средств на кредитную карту
        /// </summary>
        /// <param name="nameCard"></param>
        /// <param name="number"></param>
        /// <param name="sum"></param>
        /// <returns>Денежные средства зачислены на кредитную карту</returns>
        /// <remarks>
        /// 
        ///     GET /creditcard
        /// 
        /// Образец ввовда данных:
        /// 
        ///     nameCard: Сбер                  Введите названия кредитной карты
        ///     numbe: 0000 0000 00000 00000    Введите номер кредитной карты
        ///     sum: 00.00                      Введите сумму для зачисления на кредитную карту
        /// 
        /// </remarks>
        /// <response code="201"> Денежные средства зачислены. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        [HttpPost]
        [Route("{nameCard}/{number}/{sum}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EnrollmentСreditСard(string nameCard, string number, decimal sum)
        {
            if (sum < 0) return BadRequest($"Сумма: [{sum}] для зачисления, не может быть меньше нуля");
            var creditСard = await _creditСardSer.Service_Enrollment(nameCard, number, sum);
            if (creditСard.Result == null) return BadRequest(creditСard); //
            return CreatedAtAction(nameof(GetСreditСards), creditСard);//(GetСreditСard)?
        }
    }
}
