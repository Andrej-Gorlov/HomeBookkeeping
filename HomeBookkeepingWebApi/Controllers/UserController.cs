using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeBookkeepingWebApi.Controllers
{
    [Route("api/user")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userSer;
        public UserController(IUserService userSer)=> _userSer = userSer;


        /// <summary>
        /// Список всех пользователей.
        /// </summary>
        /// <returns>Вывод всех пользователей</returns>
        /// <remarks>
        ///
        ///     GET /user
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers() => Ok(await _userSer.Service_Get());

        /// <summary>
        /// Вывод пользователя по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Вывод данных пользователя</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /user
        ///     
        ///     {
        ///        "Id": int // Введите id пользователя, которого нужно показать.
        ///     }
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Пользователь не найдена </response>
        [HttpGet]
        [Route("e/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            if (id <= 0) return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            var user = await _userSer.Service_GetById(id);
            if (user.Result == null) return BadRequest(user);
            return Ok(user);
        }


        /// <summary>
        /// Создание нового пользователя.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Создаётся пользователь</returns>
        /// <remarks>
        /// Образец ввовда данных:
        ///
        ///     POST /user
        ///
        /// </remarks>
        /// <response code="201"> Пользователь создан. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            var user = await _userSer.Service_Create(userDTO);
            if (user.Result == null) return BadRequest(user); //
            return CreatedAtAction(nameof(GetUsers), userDTO);//(GetUsers)?
        }

        /// <summary>
        /// Редактирование пользователя.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Обновление пользователя.</returns>
        /// <remarks>
        /// Образец ввовда данных:
        ///
        ///     PUT /user
        ///
        /// </remarks>
        /// <response code="200"> Пользователь обновлен. </response>
        /// <response code="404"> Пользователь не найден. </response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            var user = await _userSer.Service_Update(userDTO);
            if (user.Result == null) return BadRequest(user);
            return Ok(user);
        }


        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Пользователь удаляется.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /creditcard
        ///     
        ///     {
        ///        "Id": int // Введите id пользователя, которого нужно удалить.
        ///     }
        ///     
        /// </remarks>
        /// <response code="204"> Пользователь удалён. (нет содержимого) </response>
        /// <response code="404"> Пользователь c указанным id не найден. </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        [HttpDelete]
        [Route("w/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0) return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            var user = await _userSer.Service_Delete(id);
            if (user.Result == false) return NotFound(user);
            return NoContent();
        }


        /// <summary>
        /// Вывод данных о пользователя по полному имени.
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>Вывод данных пользователя</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /user
        ///     
        ///     {
        ///        "fullName": Иван Иванов // Введите полное имя пользователя, которого нужно показать.
        ///     }
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Пользователь не найден. </response>
        [HttpGet]
        [Route("q/{fullName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFullNameUser(string fullName)
        {
            var user = await _userSer.Service_GetByFullName(fullName);
            if (user.Result == null) return BadRequest(user);
            return Ok(user);
        }
    }
}
