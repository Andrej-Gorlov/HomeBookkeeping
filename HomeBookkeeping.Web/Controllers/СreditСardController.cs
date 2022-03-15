using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.ViewModels;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HomeBookkeeping.Web.Controllers
{
    public class СreditСardController : Controller
    {
        private readonly IСreditСardService _creditСardService;
        public СreditСardController(IСreditСardService creditСardService) => _creditСardService = creditСardService;


        public async Task<IActionResult> СreditСardIndex()
        {
            List<СreditСardDTOBase> listСreditСard = new();
            var respons = await _creditСardService.GetСreditСardsAsync<ResponseBase>();
            if (respons != null)
            {
                listСreditСard = JsonConvert.DeserializeObject<List<СreditСardDTOBase>>(Convert.ToString(respons.Result));
            }
            return View(listСreditСard);
        }
        public async Task <IActionResult> СreditСardUserIndex(string fullName)
        {
            List<СreditСardDTOBase> listСreditСard = new();
            var respons = await _creditСardService.GetСreditСardsAsync<ResponseBase>(fullName);
            if (respons != null)
            {
                listСreditСard = JsonConvert.DeserializeObject<List<СreditСardDTOBase>>(Convert.ToString(respons.Result));
            }
            return View(listСreditСard);
        }



        public async Task<IActionResult> СreditСardUserCreate(string userName, int userId)
        {
            СreditСardDTOBase сreditСard = new()
            {
                UserId= userId,
                UserFullName=userName
            };
            return View(сreditСard);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> СreditСardUserCreate(СreditСardDTOBase model)
        {
            if (ModelState.IsValid)
            {
                var respons = await _creditСardService.CreateСreditСardAsync<ResponseBase>(model);
                if (respons != null)
                    return RedirectToAction(nameof(СreditСardUserIndex), new { fullName =model.UserFullName});
            }
            return View(model);
        }



        public async Task<IActionResult> СreditСardCreate()
        {
            List<СreditСardDTOBase> listСreditСard = new();
            var respons = await _creditСardService.GetСreditСardsAsync<ResponseBase>();
            if (respons != null)
            {
                listСreditСard = JsonConvert.DeserializeObject<List<СreditСardDTOBase>>(Convert.ToString(respons.Result));
            }
            СreditСardVM сreditСardVM = new()
            {
                СreditСard = new(),
                FullNameList = listСreditСard.Select(x => new SelectListItem
                {
                    Text = x.UserFullName,
                    Value = x.UserId.ToString(),
                }).DistinctBy(x => x.Text)
            };
            return View(сreditСardVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> СreditСardCreate(СreditСardVM model)
        {
            //-------------------- чтобы найти имя пользователя (т.к пока не знаю как передать два аргумента из выпадающего списка)
            List<СreditСardDTOBase> listСreditСard = new();
            var res = await _creditСardService.GetСreditСardsAsync<ResponseBase>();
            if (res != null)
            {
                listСreditСard = JsonConvert.DeserializeObject<List<СreditСardDTOBase>>(Convert.ToString(res.Result));
            }
            var nameUser = listСreditСard.FirstOrDefault(x => x.UserId == model.СreditСard.UserId);
            model.СreditСard.UserFullName = nameUser.UserFullName;
            //--------------------------------------------------------------------------------

            if (ModelState.IsValid)
            {
                var respons = await _creditСardService.CreateСreditСardAsync<ResponseBase>(model.СreditСard);
                if (respons != null)
                    return RedirectToAction(nameof(СreditСardIndex));
            }
            return View(model);
        }



        public async Task<IActionResult> СreditСardEdit(int creditСardId, bool isPageUser=false)
        {
            var respons = await _creditСardService.GetByIdСreditСardAsync<ResponseBase>(creditСardId);
            if (respons != null)
            {
                СreditСardDTOBase? model = JsonConvert.DeserializeObject<СreditСardDTOBase>(Convert.ToString(respons.Result));
                model.IsPageUser = isPageUser;
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> СreditСardEdit(СreditСardDTOBase model)
        {
            if (ModelState.IsValid)
            {
                var respons = await _creditСardService.UpdateСreditСardAsync<ResponseBase>(model);
                if (respons != null)
                    if(model.IsPageUser)
                       return RedirectToAction(nameof(СreditСardIndex));
                    else
                       return RedirectToAction(nameof(СreditСardUserIndex), new { fullName = model.UserFullName });
            }
            return View(model);
        }



        public async Task<IActionResult> СreditСardDelete(int creditСardId, bool isPageUser=false)
        {
            var respons = await _creditСardService.GetByIdСreditСardAsync<ResponseBase>(creditСardId);
            if (respons != null)
            {
                СreditСardDTOBase? model = JsonConvert.DeserializeObject<СreditСardDTOBase>(Convert.ToString(respons.Result));
                model.IsPageUser = isPageUser;
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> СreditСardDelete(СreditСardDTOBase model)
        {
            if (ModelState.IsValid)
            {
                var respons = await _creditСardService.DeleteСreditСardAsync<ResponseBase>(model.СreditСardId);
                if (model.IsPageUser)
                    return RedirectToAction(nameof(СreditСardIndex));
                else
                    return RedirectToAction(nameof(СreditСardUserIndex), new { fullName = model.UserFullName });
            }
            return View(model);
        }



        public async Task<IActionResult> СreditСardEnrollment(int creditСardId, bool isPageUser = false)
        {
            var respons = await _creditСardService.GetByIdСreditСardAsync<ResponseBase>(creditСardId);
            if (respons != null)
            {
                СreditСardDTOBase? model = JsonConvert.DeserializeObject<СreditСardDTOBase>(Convert.ToString(respons.Result));
                model.IsPageUser = isPageUser;
                model.Sum = 0;
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> СreditСardEnrollment(СreditСardDTOBase model)
        {
            if (ModelState.IsValid)
            {
                var respons = await _creditСardService.EnrollmentСreditСardAsync<ResponseBase>(model.BankName,model.Number,model.Sum);
                if (model.IsPageUser)
                    return RedirectToAction(nameof(СreditСardIndex));
                else
                    return RedirectToAction(nameof(СreditСardUserIndex), new { fullName = model.UserFullName });
            }
            return View(model);
        }
    }
}
