using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeBookkeeping.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)=> _userService = userService;

        public async Task <IActionResult> UserIndex()
        {
            List<UserDTOBase> listUsers = new();
            var respons = await _userService.GetUsersAsync<ResponseBase>();
            if (respons != null)
            {
                listUsers = JsonConvert.DeserializeObject<List<UserDTOBase>>(Convert.ToString(respons.Result));
            }
            return View(listUsers);

        }
        public async Task<IActionResult> UserCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate(UserDTOBase model)
        {
            if (ModelState.IsValid)
            {
                var respons = await _userService.CreateUserAsync<ResponseBase>(model);
                if (respons != null)
                    return RedirectToAction(nameof(UserIndex));
            }
            return View(model);
        }



        public async Task<IActionResult> UserEdit(int userId)
        {
            var respons = await _userService.GetByIdUserAsync<ResponseBase>(userId);
            if (respons != null)
            {
                UserDTOBase? model = JsonConvert.DeserializeObject<UserDTOBase>(Convert.ToString(respons.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(UserDTOBase model)
        {
            if (ModelState.IsValid)
            {
                var respons = await _userService.UpdateUserAsync<ResponseBase>(model);
                if (respons != null)
                    return RedirectToAction(nameof(UserIndex));
            }
            return View(model);
        }




        public async Task<IActionResult> UserDelete(int userId)
        {          
            var respons = await _userService.GetByIdUserAsync<ResponseBase>(userId);
            if (respons != null)
            {
                UserDTOBase? model = JsonConvert.DeserializeObject<UserDTOBase>(Convert.ToString(respons.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDelete(UserDTOBase model)
        {
            if (ModelState.IsValid)
            {
                var respons = await _userService.DeleteUserAsync<ResponseBase>(model.UserId);
                return RedirectToAction(nameof(UserIndex));
            }
            return View(model);
        }
    }
}
