using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Response;
using HomeBookkeepingWebApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Service.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRep;
        public UserService(IUserRepository userRep) => _userRep = userRep;

        public async Task<IBaseResponse<UserDTO>> Service_Create(UserDTO entity)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO model = await _userRep.Create(entity);
            baseResponse.DisplayMessage = "Пользователь создан";
            baseResponse.Result = model;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> Service_Delete(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _userRep.Delete(id);
            if (IsSuccess) baseResponse.DisplayMessage = "Пользователь удален.";
            if (!IsSuccess) baseResponse.DisplayMessage = $"Пользователь c указанным id: {id} не найдена.";
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<UserDTO>>> Service_Get()
        {
            var baseResponse = new BaseResponse<IEnumerable<UserDTO>>();
            IEnumerable<UserDTO> usersDTO = await _userRep.Get();
            if (usersDTO == null) baseResponse.DisplayMessage = "Список всех пользователей пуст.";
            else baseResponse.DisplayMessage = "Список всех пользователей.";
            baseResponse.Result = usersDTO;
            return baseResponse;
        }

        public async Task<IBaseResponse<UserDTO>> Service_GetByFullName(string fullName)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO model = await _userRep.GetByFullName(fullName);
            if (model == null) baseResponse.DisplayMessage = $"Пользователь под именим [{fullName}] не найден.";
            else baseResponse.DisplayMessage = $"Вывод пользователя под именим [{fullName}]";
            baseResponse.Result = model;
            return baseResponse;
        }

        public async Task<IBaseResponse<UserDTO>> Service_GetById(int id)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO model = await _userRep.GetById(id);
            if (model == null) baseResponse.DisplayMessage = $"Пользователь под id [{id}] не найдена";
            else baseResponse.DisplayMessage = $"Вывод пользователя под id [{id}]";
            baseResponse.Result = model;
            return baseResponse;
        }

        public async Task<IBaseResponse<UserDTO>> Service_Update(UserDTO entity)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO model = await _userRep.Update(entity);
            baseResponse.DisplayMessage = "Пользователь обновился.";
            baseResponse.Result = model;
            return baseResponse;
        }
    }
}
